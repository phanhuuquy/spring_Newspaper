using Microsoft.AspNetCore.Mvc;
using UC.LADI.MAG.WEB.Services.PublicImage;
using UC.LADI.MAG.WEB.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using UC.LADI.MAG.WEB.Services.PublicImage;
using Microsoft.Extensions.Caching.Memory;
using Uc.Api.Core.Controllers.Api;
using UC.Core.Interfaces;
using UC.LADI.MAG.WEB.Services;


[Route("api/public/image")]
[ApiController]
public class PublicImageController : ControllerBase
{
    private readonly IUserProvider _userProvider;
    private readonly IServiceWrapper _service;
    private readonly ILogger<PublicImageController> _logger;
    private readonly IMemoryCache _cache;

    public PublicImageController(IServiceWrapper service, ILogger<PublicImageController> logger, IUserProvider userProvider, IMemoryCache cache) 
    {
        _service = service;
        _logger = logger;
        _userProvider = userProvider;
        _cache = cache;
    }

    [HttpGet("top-hot")]
    public async Task<IActionResult> GetTopHotImages()
    {
        var images = await _service.PublicImage.GetAllItemsAsync();
        if (images == null || images.Count == 0)
        {
            return NotFound(new { message = "No hot images found" });
        }

        return Ok(images);
    }
}
