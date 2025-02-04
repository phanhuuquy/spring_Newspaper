using Microsoft.AspNetCore.Mvc;
using System;
using UC.LADI.MAG.WEB.Common;

namespace UC.LADI.MAG.WEB.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PHomeController : Controller
    {
        public PHomeController()
        {

        }

        public IActionResult Index()
        {
            return View(PublishViewPaths.home + "Index.cshtml");
        }
    }
}
