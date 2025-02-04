using Microsoft.AspNetCore.Mvc;
using UC.LADI.MAG.WEB.Common;

namespace UC.LADI.MAG.WEB.Controllers.Admin.Home
{
    [ApiExplorerSettings(IgnoreApi = true)]
	[Area("Admin")]
    public class HomeController : Controller
	{
		public HomeController()
		{

		}

        public IActionResult Index()
		{
			return View(AdminViewPaths.admin_home + "Index.cshtml");
		}
    }
}
