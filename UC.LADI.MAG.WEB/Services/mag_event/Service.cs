using UC.Core.Interfaces;
using UC.LADI.MAG.WEB.Services.DbContext.master;
using UC.LADI.MAG.WEB.Services;
using UC.Core.Helpers.QueryBuilder;

namespace UC.LADI.MAG.WEB.Services.mag_event
{
    public class Service : Repository<Guid, Entities.mag_event>, mag_event.IService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        private readonly ILogger<ServiceWrapper> _logger;

        public Service(UnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IUserProvider userProvider, ILogger<ServiceWrapper> logger)
            : base(unitOfWork, dateTimeProvider, userProvider)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userProvider;
            _logger = logger;
        }

        public async Task<List<Entities.mag_event>> GetAllEventsAsync()
        {
            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                .Select("*")
                .From("mag_event");

            var sql = sqlQueryBuider.Build();
            _logger.LogInformation($"Generated SQL Query: {sql}");

            return await _unitOfWork.Repository.QueryListAsync<Entities.mag_event>(sql, null);
        }

        public async Task<Entities.mag_event> GetEventByIdAsync(Guid id)
        {
            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                .Select("*")
                .From("mag_event")
                .Where("id = @id");

            var sql = sqlQueryBuider.Build();
            _logger.LogInformation($"Generated SQL Query: {sql}");

            return await _unitOfWork.Repository.QueryFirstAsync<Entities.mag_event>(sql, new { id });
        }

        public async Task<Entities.mag_event> CreateEventAsync(Entities.mag_event eventItem)
        {
            eventItem.id = eventItem.id == Guid.Empty ? Guid.NewGuid() : eventItem.id;

            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                .InsertInto("mag_event", new List<string> { "id", "name", "start_date", "end_date", "description", "avatar", "tenant" });

            var sql = sqlQueryBuider.Build();
            _logger.LogInformation($"Generated SQL Query: {sql}");

            var param = new
            {
                id = eventItem.id,
                name = eventItem.name,
                start_date = eventItem.start_date,
                end_date = eventItem.end_date,
                description = eventItem.description,
                avatar = eventItem.avatar,
                address = eventItem.address, 
                year = eventItem.year,
                tenant = eventItem.tenant
            };

            await _unitOfWork.Repository.ExecuteAsync(sql, param);
            return eventItem;
        }




        public async Task<Entities.mag_event> UpdateEventAsync(Guid id, Entities.mag_event eventItem)
        {
            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider( "mag", "PostgreSql")
                .Update("mag_event")
                .Set(new List<string> { "name", "start_date", "end_date", "description", "tenant" })
                .Where("id = @id");

            var sql = sqlQueryBuider.Build();
            _logger.LogInformation($"Generated SQL Query: {sql}");

            var param = new
            {
                id,
                eventItem.name,
                eventItem.start_date,
                eventItem.end_date,
                eventItem.description,
                avatar = eventItem.avatar,
                address = eventItem.address,
                year = eventItem.year,
                eventItem.tenant
            };

            await _unitOfWork.Repository.ExecuteAsync(sql, param);
            return eventItem;
        }

        public async Task DeleteEventAsync(Guid id)
        {
            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                .DeleteFrom("mag_event")
                .Where("id = @id");

            var sql = sqlQueryBuider.Build();
            _logger.LogInformation($"Generated SQL Query: {sql}");

            await _unitOfWork.Repository.ExecuteAsync(sql, new { id });
        }
    }
}
