using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UC.LADI.MAG.WEB.Services;
using Microsoft.Extensions.Logging;

[Route("api/public/collection")]
[ApiController]
public class PublicCollectionController : ControllerBase
{
    private readonly IServiceWrapper _service;
    private readonly ILogger<PublicCollectionController> _logger;

    public PublicCollectionController(IServiceWrapper service, ILogger<PublicCollectionController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCollections()
    {
        var collections = await _service.PublicCollection.GetAllCollectionsAsync();
        if (collections == null || collections.Count == 0)
        {
            return NotFound(new { message = "No collections found" });
        }

        return Ok(collections);
    }

    [HttpGet("{collectionId}/items")]
    public async Task<IActionResult> GetCollectionWithItems(Guid collectionId)
    {
        var collectionWithItems = await _service.PublicCollection.GetCollectionWithItemsAsync(collectionId);
        if (collectionWithItems == null || collectionWithItems.Items.Count == 0)
        {
            return NotFound(new { message = "No items found for this collection" });
        }

        return Ok(collectionWithItems);
    }
}
