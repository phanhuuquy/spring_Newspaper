using Microsoft.AspNetCore.Mvc;
using UC.LADI.MAG.WEB.Common;

namespace UC.LADI.MAG.WEB.Controllers.Admin.Images
{
    [Area("Admin")]
    public class ImagesController : Controller
    {
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(ILogger<ImagesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(AdminViewPaths.images + "Index.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}