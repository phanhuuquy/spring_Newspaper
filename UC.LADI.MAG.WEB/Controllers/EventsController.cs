using Microsoft.AspNetCore.Mvc;
using UC.LADI.MAG.WEB.Common;

namespace UC.LADI.MAG.WEB.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class EventsController : Controller
    {
        private readonly ILogger<EventsController> _logger;

        public EventsController(ILogger<EventsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Spring()
        {
            return View(PublishViewPaths.events + "Spring/Index.cshtml");
        }

        public IActionResult Read() {
            return View(PublishViewPaths.events + "Read/Index.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}