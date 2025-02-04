using UC.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using UC.LADI.MAG.WEB.Controllers;
using UC.LADI.MAG.WEB.Services;
using UC.LADI.MAG.WEB.Entities;


namespace Uc.Api.Core.Controllers.Api
{
	public class MagCollectionItemController : ApiControllerCore<Guid, mag_collection_item>
	{
		private readonly IUserProvider _userProvider;
		private readonly IServiceWrapper _service;
		private readonly ILogger<MagCollectionItemController> _logger;
        private readonly IMemoryCache _cache;
        public MagCollectionItemController(IServiceWrapper service, ILogger<MagCollectionItemController> logger, IUserProvider userProvider, IMemoryCache cache) : base(service, logger, userProvider, cache)
		{
			_service = service;
			_logger = logger;
			_userProvider = userProvider;
			_cache = cache;
		}
    }
}
