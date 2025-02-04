using System.Collections.Generic;
using System.Threading.Tasks;
using UC.Core.Helpers.QueryBuilder;
using UC.LADI.MAG.WEB.Dtos;
using UC.LADI.MAG.WEB.Services.DbContext.master;
using Microsoft.Extensions.Logging;

namespace UC.LADI.MAG.WEB.Services.PublicCollection
{
    public class Service : Repository<Guid, UC.LADI.MAG.WEB.Entities.mag_collection>, IService
    {
        private readonly ILogger<ServiceWrapper> _logger;
        private readonly UnitOfWork _unitOfWork;

        public Service(UnitOfWork unitOfWork, ILogger<ServiceWrapper> logger)
            : base(unitOfWork, null, null)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<CollectionDto>> GetAllCollectionsAsync()
        {
            try
            {
                SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                    .Select("id, name, order_index")
                    .From("mag_collection")
                    .OrderBy("order_index", "ASC");

                var sql = sqlQueryBuider.Build();
                _logger.LogInformation($"Generated SQL Query: {sql}");

                var collections = await _unitOfWork.Repository.QueryListAsync<CollectionDto>(sql, null);
                return collections;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching collections.");
                throw;
            }
        }

        public async Task<CollectionWithItemsDto> GetCollectionWithItemsAsync(Guid collectionId)
        {
            try
            {
                SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                    .Select("c.id AS CollectionId, c.name AS CollectionName, i.id, i.title, i.description, i.avatar, i.path, i.order_index")
                    .From("mag_collection_item ci")
                    .InnerJoin("mag_collection c", "ci.collection_id = c.id")
                    .InnerJoin("mag_item i", "ci.item_id = i.id")
                    .Where("ci.collection_id = @collectionId")
                    .OrderBy("i.order_index", "ASC")
                    .Paging(1, 10);

                var sql = sqlQueryBuider.Build();
                _logger.LogInformation($"Generated SQL Query: {sql}");

                var items = await _unitOfWork.Repository.QueryListAsync<ItemDto>(sql, new { collectionId });

                return new CollectionWithItemsDto
                {
                    CollectionId = collectionId,
                    Items = items
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching collection with items.");
                throw;
            }
        }
    }
}
