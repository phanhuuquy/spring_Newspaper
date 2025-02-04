using UC.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using UC.LADI.MAG.WEB.Controllers;
using UC.LADI.MAG.WEB.Services;
using UC.LADI.MAG.WEB.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using UC.Core.Models;
using Microsoft.AspNetCore.Authorization;
using static Dapper.SqlMapper;
using UC.Core.Common;

namespace Uc.Api.Core.Controllers.Api
{
    public class MagImageController : ApiControllerCore<Guid, mag_image>
    {
        private readonly IUserProvider _userProvider;
        private readonly IServiceWrapper _service;
        private readonly ILogger<MagImageController> _logger;
        private readonly IMemoryCache _cache;

        public MagImageController(IServiceWrapper service, ILogger<MagImageController> logger, IUserProvider userProvider, IMemoryCache cache) : base(service, logger, userProvider, cache)
        {
            _service = service;
            _logger = logger;
            _userProvider = userProvider;
            _cache = cache;
        }

        [HttpGet("v2/GetAllImages")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllImages()
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var images = await _service.mag_image.GetAllImagesAsync();
                return ResponseMessage.Success(images);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpGet("v2/GetImageById/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetImageById(Guid id)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var image = await _service.mag_image.GetImageByIdAsync(id);
                if (image == null)
                {
                    return ResponseMessage.Warning($"Image with ID {id} not found.");
                }
                return ResponseMessage.Success(image);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPost("v2/CreateImage")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateImage([FromBody] mag_image image)
        {

            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var createdImage = await _service.mag_image.CreateImageAsync(image);

                return ResponseMessage.Success(createdImage, "Image created successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPost("v2/UploadAvatar")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadAvatar(IFormFile avatar)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                if (avatar == null || avatar.Length == 0)
                {
                    return ResponseMessage.Error("File không hợp lệ.");
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/image/spring");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                    _logger.LogInformation($"Created folder: {uploadsFolder}");
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{avatar.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await avatar.CopyToAsync(fileStream);
                }

                var relativePath = $"/img/events/spring/{avatar.FileName}";
                _logger.LogInformation($"File saved at: {filePath}");
                return ResponseMessage.Success(relativePath, "File uploaded successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPut("v2/UpdateImage/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateImage(Guid id, [FromBody] mag_image image)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var updatedImage = await _service.mag_image.UpdateImageAsync(id, image);
                if (updatedImage == null)
                {
                    return ResponseMessage.Warning($"Image with ID {id} not found.");
                }
                return ResponseMessage.Success(updatedImage, "Image updated successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpDelete("v2/DeleteImage/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var image = await _service.mag_image.GetImageByIdAsync(id);
                if (image == null)
                {
                    return ResponseMessage.Warning($"Image with ID {id} not found.");
                }

                await _service.mag_image.DeleteImageAsync(id);
                return ResponseMessage.Success(null, "Image deleted successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
