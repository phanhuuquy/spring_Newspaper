using Microsoft.AspNetCore.Mvc;
using UC.LADI.MAG.WEB.Common;

namespace UC.LADI.MAG.WEB.Controllers.Admin.Collections
{
    [Area("Admin")]
    public class CollectionsController : Controller
    {
        private readonly ILogger<CollectionsController> _logger;



        public IActionResult Index()
        {
            return View(AdminViewPaths.collections + "Index.cshtml");
        }

       
    }
}