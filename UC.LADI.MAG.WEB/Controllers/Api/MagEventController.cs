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

namespace Uc.Api.Core.Controllers.Api
{
    public class MagEventController : ApiControllerCore<Guid, mag_event>
    {
        private readonly IUserProvider _userProvider;
        private readonly IServiceWrapper _service;
        private readonly ILogger<MagEventController> _logger;
        private readonly IMemoryCache _cache;

        public MagEventController(IServiceWrapper service, ILogger<MagEventController> logger, IUserProvider userProvider, IMemoryCache cache) : base(service, logger, userProvider, cache)
        {
            _service = service;
            _logger = logger;
            _userProvider = userProvider;
            _cache = cache;
        }

        [HttpGet("v2/GetAllEvents")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllEvents()
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var events = await _service.mag_event.GetAllEventsAsync();
                return ResponseMessage.Success(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpGet("v2/GetEventById/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var eventItem = await _service.mag_event.GetEventByIdAsync(id);
                if (eventItem == null)
                {
                    return ResponseMessage.Warning($"Event with ID {id} not found.");
                }
                return ResponseMessage.Success(eventItem);
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

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/events/spring");
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

                var relativePath = $"/img/events/spring/{uniqueFileName}";
                _logger.LogInformation($"File saved at: {filePath}");
                return ResponseMessage.Success(relativePath, "File uploaded successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }


        [HttpPost("v2/CreateEvent")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateEvent([FromBody] mag_event eventItem)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            _logger.LogInformation($"Received event data: Name={eventItem.name}, Address={eventItem.address}, Year={eventItem.year}");

            try
            {
                var createdEvent = await _service.mag_event.CreateEventAsync(eventItem);
                return ResponseMessage.Success(createdEvent, "Event created successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }




        [HttpPut("v2/UpdateEvent/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] mag_event eventItem)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var updatedEvent = await _service.mag_event.UpdateEventAsync(id, eventItem);
                if (updatedEvent == null)
                {
                    return ResponseMessage.Warning($"Event with ID {id} not found.");
                }
                return ResponseMessage.Success(updatedEvent, "Event updated successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }


        [HttpDelete("v2/DeleteEvent/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                await _service.mag_event.DeleteEventAsync(id);
                return ResponseMessage.Success(null, "Event deleted successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
