using UC.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using UC.LADI.MAG.WEB.Controllers;
using UC.LADI.MAG.WEB.Services;
using UC.LADI.MAG.WEB.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System;
using System.Threading.Tasks;
using UC.Core.Models;
using Microsoft.AspNetCore.Authorization;
using UC.LADI.MAG.WEB.Dtos;

namespace Uc.Api.Core.Controllers.Api
{
    public class MagItemController : ApiControllerCore<Guid, mag_item>
    {
        private readonly IUserProvider _userProvider;
        private readonly IServiceWrapper _service;
        private readonly ILogger<MagItemController> _logger;
        private readonly IMemoryCache _cache;

        public MagItemController(IServiceWrapper service, ILogger<MagItemController> logger, IUserProvider userProvider, IMemoryCache cache) : base(service, logger, userProvider, cache)
        {
            _service = service;
            _logger = logger;
            _userProvider = userProvider;
            _cache = cache;
        }

        [HttpGet("v2/GetAllItems")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllItems()
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var itemsWithCollections = await _service.mag_item.GetAllItemsAsync();
                return ResponseMessage.Success(itemsWithCollections);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }


        [HttpGet("v2/GetItemById/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetItemById(Guid id)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var itemWithCollections = await _service.mag_item.GetItemByIdAsync(id);

                if (itemWithCollections == null)
                {
                    return ResponseMessage.Warning($"Item with ID {id} not found.");
                }

                return ResponseMessage.Success(itemWithCollections);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }


        [HttpPost("v2/UploadFile")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                if (file == null || file.Length == 0)
                {
                    return ResponseMessage.Error("File không hợp lệ.");
                }

                var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var allowedPdfExtension = ".pdf";
                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                string uploadsFolder;
                if (allowedImageExtensions.Contains(fileExtension))
                {
                    uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/item/spring");
                }
                else if (fileExtension == allowedPdfExtension)
                {
                    uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/item/PDF");
                }
                else
                {
                    return ResponseMessage.Error("Chỉ chấp nhận file ảnh (.jpg, .jpeg, .png, .gif) hoặc PDF.");
                }

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var relativePath = fileExtension == allowedPdfExtension
                    ? $"/img/item/PDF/{uniqueFileName}"
                    : $"/img/item/spring/{uniqueFileName}";

                _logger.LogInformation($"File saved at: {filePath}");
                return ResponseMessage.Success(relativePath, "File uploaded successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }




        [HttpPost("v2/CreateItem")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemRequest request)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var createdItem = await _service.mag_item.CreateItemAsync(request.Item, request.CollectionIds);
                return ResponseMessage.Success(createdItem, "Item created successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPut("v2/UpdateItem/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateItem(Guid id, [FromBody] UpdateItemRequest request)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var updatedItem = await _service.mag_item.UpdateItemAsync(id, request.Item, request.CollectionIds);
                if (updatedItem == null)
                {
                    return ResponseMessage.Warning($"Item with ID {id} not found.");
                }
                return ResponseMessage.Success(updatedItem, "Item updated successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }




        [HttpDelete("v2/DeleteItem/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                await _service.mag_item.DeleteItemAsync(id);
                return ResponseMessage.Success(null, "Item deleted successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpGet("v2/GetCollectionItemsByItemId/{itemId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCollectionItemsByItemId(Guid itemId)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var collectionItems = await _service.mag_item.GetCollectionItemsByItemIdAsync(itemId);
                if (!collectionItems.Any())
                {
                    return ResponseMessage.Warning($"No collections found for item ID {itemId}.");
                }
                return ResponseMessage.Success(collectionItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

    }
}
