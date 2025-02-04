using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UC.Core.Common;
using UC.Core.Models.Ums;
using UC.Core.Models;
using UC.Core.Helpers;
using UC.Core.Models.Sidebar;

namespace UC.LADI.MAG.WEB.ViewComponents
{
    [ViewComponent]
    public class Sidebar : ViewComponent
    {
        private readonly IConfiguration _configuration;
        public Sidebar(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        private UserPermInfo GetUserPermInfoWithStaticFile(string userName, string accessToken)
        {
            UserPermInfo userPerm = new UserPermInfo();
            try
            {
                string pathUserPerm = Path.Combine(Directory.GetCurrentDirectory(), "static", "UserPermInfo", userName + ".json");
                if (Path.Exists(pathUserPerm))
                {
                    using (StreamReader r = new StreamReader(pathUserPerm))
                    {
                        string json = r.ReadToEnd();
                        userPerm = JsonConvert.DeserializeObject<UserPermInfo>(json);
                    }
                }
                else
                {
                    userPerm = GetUserPermInfo(userName, accessToken);
                }
            }
            catch (Exception ex) { }
            return userPerm;
        }

        private UserPermInfo GetUserPermInfo(string userName, string accessToken)
        {
            UserPermInfo userPerm = new UserPermInfo();
            string address = _configuration.GetSection("Services:ApiCore").Value;
            ClientRequestInfo clientRequestInfo = new ClientRequestInfo(address);
            clientRequestInfo.Bearer = "bearer";
            clientRequestInfo.Token = accessToken;
            HttpClientBuilder httpClientBuilder = new HttpClientBuilder(clientRequestInfo);
            ClientResponseInfo clientResponseInfo = httpClientBuilder.GetAsync("Ums_Auth/get-perm").GetAwaiter().GetResult();
            if (clientResponseInfo.IsStatusCode)
            {
                ClientResponseResult<UserPermInfo> responseResult = JsonConvert.DeserializeObject<ClientResponseResult<UserPermInfo>>(clientResponseInfo.Content);
                if (responseResult.Success)
                {
                    if (responseResult.Data != null)
                    {
                        userPerm = responseResult.Data;
                    }
                }
            }
            return userPerm;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            SidebarViewModel model = new SidebarViewModel();
            string address = _configuration.GetSection("Services:ApiCore").Value;
            string appCode = _configuration.GetSection("AppConfig:AppCode").Value;
            string authentication_code = _configuration.GetSection("AppConfig:AuthenticationCode").Value;

            ClientRequestInfo clientRequestInfo = new ClientRequestInfo(address);
            clientRequestInfo.Bearer = "bearer";
            clientRequestInfo.Token = HttpContext.Session.GetString(SessionKeys.AccessToken);
            HttpClientBuilder httpClientBuilder = new HttpClientBuilder(clientRequestInfo);
            ClientResponseInfo clientResponseInfo = httpClientBuilder.GetAsync("Ums_Menu/get-sidebar?app_code=" + appCode).GetAwaiter().GetResult();
            if (clientResponseInfo.IsStatusCode)
            {
                ClientResponseResult<List<SidebarTree>> responseResult = JsonConvert.DeserializeObject<ClientResponseResult<List<SidebarTree>>>(clientResponseInfo.Content);
                if (responseResult.Success)
                {
                    if (responseResult.Data != null)
                    {
                        model.Sidebar = responseResult.Data;
                    }
                }
            }
            string userName = HttpContext.Session.GetString(SessionKeys.UserName);
            if (userName != "admin")
            {
                if (authentication_code == "blib")
                {
                    model.UserPerm = GetUserPermInfoWithStaticFile(userName, clientRequestInfo.Token);
                }
                else if (authentication_code == "core")
                {
                    model.UserPerm = GetUserPermInfo(userName, clientRequestInfo.Token);
                }
            }
            else if (userName == "admin")
            {
                model.UserPerm = new UserPermInfo();
                if (model.Sidebar != null && model.Sidebar.Count > 0)
                {
                    model.UserPerm.menus = model.Sidebar.Traverse(o => o.children).Select(o => o.id).ToList();
                }
            }

            model.MenuActive = HttpContext.Request.Path;
            return View(model);
        }
    }
}


