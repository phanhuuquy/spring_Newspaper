using UC.Core.Interfaces;
using UC.LADI.MAG.WEB.Services.DbContext.master;
using UC.LADI.MAG.WEB.Services;
using UC.Core.Helpers.QueryBuilder;

namespace UC.LADI.MAG.WEB.Services.mag_collection
{
    public class Service : Repository<Guid, Entities.mag_collection>, mag_collection.IService
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
        public async Task<List<Entities.mag_collection>> GetAllCollectionsAsync()
        {
            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                .Select("*")
                .From("mag_collection");

            var sql = sqlQueryBuider.Build();
            _logger.LogInformation($"Generated SQL Query: {sql}");

            return await _unitOfWork.Repository.QueryListAsync<Entities.mag_collection>(sql, null);
        }

        public async Task<Entities.mag_collection> GetCollectionByIdAsync(Guid id )
        {
            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                .Select("*")
                .From("mag_collection")
                .Where("id = @id") ;

            var sql = sqlQueryBuider.Build();
            _logger.LogInformation($"Generated SQL Query: {sql}");

            return await _unitOfWork.Repository.QueryFirstAsync<Entities.mag_collection>(sql, new { id });
        }

        public async Task<Entities.mag_collection> CreateCollectionAsync(Entities.mag_collection collection)
        {
            collection.id = collection.id == Guid.Empty ? Guid.NewGuid() : collection.id;

            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                .InsertInto("mag_collection", new List<string> { "id", "name", "order_index", "tenant" });

            var sql = sqlQueryBuider.Build();
            _logger.LogInformation($"Generated SQL Query: {sql}");

            var param = new
            {
                id = collection.id,
                name = collection.name,
                order_index = collection.order_index,
                tenant = collection.tenant
            };

            await _unitOfWork.Repository.ExecuteAsync(sql, param);
            return collection;
        }


        public async Task<Entities.mag_collection> UpdateCollectionAsync(Guid id, Entities.mag_collection collection)
        {

            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                .Update("mag_collection")
                .Set(new List<string> { "name", "order_index", "tenant" })
                .Where("id = @id");

            var sql = sqlQueryBuider.Build();
            _logger.LogInformation($"Generated SQL Query: {sql}");

            var param = new
            {
                id,
                collection.name,
                collection.order_index,
                collection.tenant
            };

            await _unitOfWork.Repository.ExecuteAsync(sql, param);
            return collection;
        }



        public async Task DeleteCollectionAsync(Guid id)
        {
            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                .DeleteFrom("mag_collection")
                .Where("id = @id");

            var sql = sqlQueryBuider.Build();
            _logger.LogInformation($"Generated SQL Query: {sql}");

            await _unitOfWork.Repository.ExecuteAsync(sql, new { id });
        }
    }
}
