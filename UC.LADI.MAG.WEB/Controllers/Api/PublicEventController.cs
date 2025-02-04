using Microsoft.AspNetCore.Mvc;
using UC.LADI.MAG.WEB.Services;
using UC.LADI.MAG.WEB.Dtos;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

[Route("api/public/event")]
[ApiController]
public class PublicEventController : ControllerBase
{
    private readonly IServiceWrapper _service;
    private readonly ILogger<PublicEventController> _logger;

    public PublicEventController(IServiceWrapper service, ILogger<PublicEventController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetPublicEvents()
    {
        var events = await _service.PublicEvent.GetPublicEventsAsync();
        if (events == null || events.Count == 0)
        {
            return NotFound(new { message = "No events found" });
        }

        return Ok(events);
    }
}
