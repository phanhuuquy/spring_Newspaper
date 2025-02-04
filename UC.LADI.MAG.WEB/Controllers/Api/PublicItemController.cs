using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UC.LADI.MAG.WEB.Services;
using UC.Core.Interfaces;

[Route("api/public/item")]
[ApiController]
public class PublicItemController : ControllerBase
{
    private readonly IServiceWrapper _service;
    private readonly ILogger<PublicItemController> _logger;

    public PublicItemController(IServiceWrapper service, ILogger<PublicItemController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("top-item")]
    public async Task<IActionResult> GetTop10Items()
    {
        var items = await _service.PublicItem.GetTop10ItemsAsync();
        if (items == null || items.Count == 0)
        {
            return NotFound(new { message = "No items found" });
        }

        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemById(Guid id)
    {
        var item = await _service.PublicItem.GetItemByIdAsync(id);
        if (item == null)
        {
            return NotFound(new { message = $"No item found with ID: {id}" });
        }

        return Ok(item);
    }

}
