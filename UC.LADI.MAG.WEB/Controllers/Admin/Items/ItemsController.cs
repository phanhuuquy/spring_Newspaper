using Microsoft.AspNetCore.Mvc;
using UC.LADI.MAG.WEB.Common;

namespace UC.LADI.MAG.WEB.Controllers.Admin.Items
{
    [Area("Admin")]
    public class ItemsController : Controller
    {
        private readonly ILogger<ItemsController> _logger;
        public IActionResult Index()
        {
            return View(AdminViewPaths.items + "Index.cshtml");
        }

     
    }
}