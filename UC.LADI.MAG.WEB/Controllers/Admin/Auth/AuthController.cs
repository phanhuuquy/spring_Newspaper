using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UC.Core.Common;
using UC.Core.Models.Ums;
using UC.Core.Models;
using UC.LADI.MAG.WEB.Common;

namespace UC.LADI.MAG.WEB.Controllers.Admin.Auth
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IDictionary<string, List<string>> _loggedUsers;

        public AuthController(IConfiguration configuration, IDictionary<string, List<string>> loggedUsers)
        {
            _configuration = configuration;
            _loggedUsers = loggedUsers;
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

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UC.Core.Models.Ums.LoginRequest loginRequest)
        {
            try
            {
                var sessionId = HttpContext.Session.Id;

                //lock (_loggedUsers)
                //{
                //    if (_loggedUsers.ContainsKey(loginRequest.UserName) && _loggedUsers[loginRequest.UserName].Any(id => id == sessionId) == false)
                //    {
                //        _loggedUsers.Remove(loginRequest.UserName);
                //    }
                //}

                loginRequest.UcKey = _configuration.GetSection("UcKey").Value;
                string address;
                string authentication_code = _configuration.GetSection("AppConfig:AuthenticationCode").Value;
                ClientRequestInfo clientRequestInfo;
                HttpClientBuilder httpClientBuilder;
                ClientResponseInfo clientResponseInfo;

                loginRequest.Type = "internal";
                address = _configuration.GetSection("Services:ApiCore").Value;
                clientRequestInfo = new ClientRequestInfo(address);
                httpClientBuilder = new HttpClientBuilder(clientRequestInfo);
                clientResponseInfo = httpClientBuilder.PostAsync("Ums_Auth/login", JsonConvert.SerializeObject(loginRequest)).GetAwaiter().GetResult();
                if (clientResponseInfo.IsStatusCode)
                {
                    ClientResponseResult<LoginResponse> responseResult = JsonConvert.DeserializeObject<ClientResponseResult<LoginResponse>>(clientResponseInfo.Content);
                    if (responseResult.Success)
                    {
                        HttpContext.Session.SetInt32(SessionKeys.IsAuthencated, 1);
                        HttpContext.Session.SetString(SessionKeys.UserName, loginRequest.UserName);
                        HttpContext.Session.SetString(SessionKeys.AccessToken, responseResult.Data.AccessToken);

                        //lock (_loggedUsers)
                        //{
                        //    if (!_loggedUsers.ContainsKey(loginRequest.UserName))
                        //    {
                        //        _loggedUsers[loginRequest.UserName] = new List<string>();
                        //    }
                        //    _loggedUsers[loginRequest.UserName].Add(sessionId);
                        //}

                        return ResponseMessage.Success(new LoginResponse(responseResult.Data.UserId, responseResult.Data.UserName, responseResult.Data.AccessToken, GetUserPermInfo(responseResult.Data.UserName, responseResult.Data.AccessToken)));
                    }
                    else
                    {
                        return ResponseMessage.Error(responseResult.Message);
                    }
                }
                else
                {
                    return ResponseMessage.Error(clientResponseInfo.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Logout(string userName)
        {
            try
            {
                var sessionId = HttpContext.Session.Id;

                if (!string.IsNullOrEmpty(userName))
                {
                    //lock (_loggedUsers)
                    //{
                    //    if (_loggedUsers.ContainsKey(userName))
                    //    {
                    //        HttpContext.Session.Clear();
                    //        _loggedUsers[userName].Remove(sessionId);
                    //        if (_loggedUsers[userName].Count == 0)
                    //        {
                    //            _loggedUsers.Remove(userName);
                    //        }
                    //    }
                    //}
                }

                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
