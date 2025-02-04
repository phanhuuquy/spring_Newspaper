
using System.Runtime.Intrinsics.X86;
using UC.Core.Interfaces;
using UC.LADI.MAG.WEB.Services.DbContext.master;

namespace UC.LADI.MAG.WEB.Services
{
    public class ServiceWrapper : IServiceWrapper
	{
        private readonly UnitOfWork _unitOfWork;
		private readonly IDateTimeProvider _dateTimeProvider;
		private readonly IUserProvider _userProvider;
        private readonly ILogger<ServiceWrapper> _logger;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public ServiceWrapper(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider, ILogger<ServiceWrapper> logger, IHttpContextAccessor httpContextAccessor)
		{
            _unitOfWork = unitOfWork;
			_dateTimeProvider = dateTimeProvider;
			_userProvider = userProvider;
            _logger = logger;
			_httpContextAccessor = httpContextAccessor;
        }

        private mag_collection.IService _mag_collection = null;
        public mag_collection.IService mag_collection => _mag_collection ?? (_mag_collection = new mag_collection.Service(_unitOfWork, _dateTimeProvider, _userProvider, _logger));

        private mag_event.IService _mag_event = null;
        public mag_event.IService mag_event => _mag_event ?? (_mag_event = new mag_event.Service(_unitOfWork, _dateTimeProvider, _userProvider, _logger));

        private mag_image.IService _mag_image = null;
        public mag_image.IService mag_image => _mag_image ?? (_mag_image = new mag_image.Service(_unitOfWork, _dateTimeProvider, _userProvider, _logger));

        private mag_item.IService _mag_item = null;
        public mag_item.IService mag_item => _mag_item ?? (_mag_item = new mag_item.Service(_unitOfWork, _dateTimeProvider, _userProvider, _logger));

        private PublicImage.IService _PublicImage = null;
        public PublicImage.IService PublicImage => _PublicImage ?? (_PublicImage = new PublicImage.Service(_unitOfWork, _logger));

        private PublicEvent.IService _PublicEvent = null;
        public PublicEvent.IService PublicEvent => _PublicEvent ??= new PublicEvent.Service(_unitOfWork, _logger);

        private PublicItem.IService _PublicItem = null;
        public PublicItem.IService PublicItem => _PublicItem ?? (_PublicItem = new PublicItem.Service(_unitOfWork, _logger));

        private PublicCollection.IService _PublicCollection = null;
        public PublicCollection.IService PublicCollection => _PublicCollection ?? (_PublicCollection = new PublicCollection.Service(_unitOfWork, _logger));
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
    }
}
