using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UC.Core.Common;
using UC.Core.Helpers;
using UC.Core.Models;
using UC.Core.Models.UCFormSearchListConfig;
using UC.LADI.MAG.WEB.ViewModels.Shared;
using UC.Core.Models.UCFormConfig;
using UC.LADI.MAG.WEB.Common;

namespace UC.LADI.MAG.WEB.ViewComponents
{
	[ViewComponent]
	public class UCForm : ViewComponent
	{
        private readonly LangResource _langResource;

        public UCForm(LangResource langResource)
        {
            _langResource = langResource;
        }

        public async Task<IViewComponentResult> InvokeAsync(string FileName, bool? isFormSearch = false, string? type = "basic")
		{
            Dictionary<string, string> lApp = _langResource != null && _langResource.data.ContainsKey("LAppController") ? _langResource.data["LAppController"] : null;

            ClientRequestInfo clientRequestInfo = new ClientRequestInfo(string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host));
            HttpClientBuilder httpClientBuilder = new HttpClientBuilder(clientRequestInfo);
            string config_type = "";
            if(isFormSearch.HasValue && isFormSearch.Value)
            {
                config_type = "uc_form_search_list";
            }   
            else
            {
                config_type = "uc_form";
            }    
            ClientResponseInfo clientResponseInfo = httpClientBuilder.GetAsync(ConstLocation.value + "/json/config/" + config_type + "/" + FileName).GetAwaiter().GetResult();
            UCFormViewModel model = new UCFormViewModel();
            if (clientResponseInfo.IsStatusCode)
            {
                if(isFormSearch.HasValue && isFormSearch.Value)
                {
                    UCFormSearchListConfig formSearchListConfig = JsonConvert.DeserializeObject<UCFormSearchListConfig>(clientResponseInfo.Content);
                    if(type == "basic")
                    {
                        if(formSearchListConfig.form != null)
                        {
                            model.html = UcHelper.DrawFormSearch(lApp, formSearchListConfig.form);
                        }
                    }
                    else if (type == "advance")
                    {
                        if(formSearchListConfig.form_advance != null)
                        {
                            model.html = UcHelper.DrawFormSearch(lApp, formSearchListConfig.form_advance);
                        }
                    }
                }
                else
                {
                    UCFormConfig formConfig = JsonConvert.DeserializeObject<UCFormConfig>(clientResponseInfo.Content);
                    model.html = UcHelper.DrawFormSearch(lApp, formConfig.form);
                }
            }

            return View(model);
		}
	}
}


