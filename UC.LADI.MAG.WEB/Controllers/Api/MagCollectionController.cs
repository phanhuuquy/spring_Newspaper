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
    public class MagCollectionController : ApiControllerCore<Guid, mag_collection>
    {
        private readonly IUserProvider _userProvider;
        private readonly IServiceWrapper _service;
        private readonly ILogger<MagCollectionController> _logger;
        private readonly IMemoryCache _cache;

        public MagCollectionController(IServiceWrapper service, ILogger<MagCollectionController> logger, IUserProvider userProvider, IMemoryCache cache) : base(service, logger, userProvider, cache)
        {
            _service = service;
            _logger = logger;
            _userProvider = userProvider;
            _cache = cache;
        }

        [HttpGet("v2/GetAllCollections")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCollections()
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var collections = await _service.mag_collection.GetAllCollectionsAsync();
                return ResponseMessage.Success(collections);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpGet("v2/GetCollectionById/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCollectionById(Guid id)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var collection = await _service.mag_collection.GetCollectionByIdAsync(id);
                if (collection == null)
                {
                    return ResponseMessage.Warning($"Collection with ID {id}  not found.");
                }
                return ResponseMessage.Success(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPost("v2/CreateCollection")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCollection([FromBody] mag_collection collection)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var createdCollection = await _service.mag_collection.CreateCollectionAsync(collection);

                return ResponseMessage.Success(createdCollection, "Collection created successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }


        [HttpPut("v2/UpdateCollection/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateCollection(Guid id, [FromBody] mag_collection collection)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                var updatedCollection = await _service.mag_collection.UpdateCollectionAsync(id, collection);
                if (updatedCollection == null)
                {
                    return ResponseMessage.Warning($"Collection with ID {id} not found.");
                }
                return ResponseMessage.Success(updatedCollection, "Collection updated successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpDelete("v2/DeleteCollection/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteCollection(Guid id)
        {
            _logger.LogInformation($"Start {MethodBase.GetCurrentMethod()?.Name}");
            try
            {
                await _service.mag_collection.DeleteCollectionAsync(id);
                return ResponseMessage.Success(null, "Collection deleted successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name} error: {ex.Message}");
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
