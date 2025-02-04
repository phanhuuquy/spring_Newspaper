using Microsoft.AspNetCore.Mvc;
using UC.LADI.MAG.WEB.Common;

namespace UC.LADI.MAG.WEB.Controllers.Admin.Auth
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LoginController : Controller
    {
        [Route("dang-nhap")]
        public IActionResult Index()
        {
            return View(AdminViewPaths.admin_auth + "Login/Index.cshtml");
        }
    }
}
