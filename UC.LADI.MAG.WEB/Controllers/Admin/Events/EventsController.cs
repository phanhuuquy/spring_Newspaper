using Microsoft.AspNetCore.Mvc;
using UC.LADI.MAG.WEB.Common;

namespace UC.LADI.MAG.WEB.Controllers.Admin.Events
{
    [Area("Admin")]
    public class EventsController : Controller
    {
        private readonly ILogger<EventsController> _logger;

        public IActionResult Index()
        {
            return View(AdminViewPaths.events + "Index.cshtml");
        }

    }
}