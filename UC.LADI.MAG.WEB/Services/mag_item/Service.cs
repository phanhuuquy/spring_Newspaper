using UC.Core.Interfaces;
using UC.LADI.MAG.WEB.Services.DbContext.master;
using UC.LADI.MAG.WEB.Services;
using UC.Core.Helpers.QueryBuilder;
using UC.LADI.MAG.WEB.Entities;

namespace UC.LADI.MAG.WEB.Services.mag_item
{
    public class Service : Repository<Guid, Entities.mag_item>, mag_item.IService
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
        public async Task<List<object>> GetAllItemsAsync()
        {

            var sqlItems = new SqlQueryBuider("mag", "PostgreSql")
                .Select("*")
                .From("mag_item")
                .Build();

            _logger.LogInformation($"Generated SQL Query for items: {sqlItems}");
            var items = await _unitOfWork.Repository.QueryListAsync<Entities.mag_item>(sqlItems, null);


            var result = new List<object>();

            foreach (var item in items)
            {

                var sqlCollectionIds = new SqlQueryBuider("mag", "PostgreSql")
                    .Select("collection_id")
                    .From("mag_collection_item")
                    .Where("item_id = @item_id")
                    .Build();

                _logger.LogInformation($"Generated SQL Query for collection IDs: {sqlCollectionIds}");
                var collectionIds = await _unitOfWork.Repository.QueryListAsync<Guid>(sqlCollectionIds, new { item_id = item.id });


                var sqlCollections = new SqlQueryBuider("mag", "PostgreSql")
                    .Select("id, name")
                    .From("mag_collection")
                    .Where("id = ANY(@collection_ids)")
                    .Build();

                _logger.LogInformation($"Generated SQL Query for collections: {sqlCollections}");
                var collections = await _unitOfWork.Repository.QueryListAsync<dynamic>(sqlCollections, new { collection_ids = collectionIds.ToArray() });


                result.Add(new
                {
                    Item = item,
                    Collections = collections
                });
            }

            return result;
        }


        public async Task<object> GetItemByIdAsync(Guid id)
        {

            var sqlItem = new SqlQueryBuider("mag", "PostgreSql")
                .Select("*")
                .From("mag_item")
                .Where("id = @id")
                .Build();

            _logger.LogInformation($"Generated SQL Query for item: {sqlItem}");
            var item = await _unitOfWork.Repository.QueryFirstAsync<Entities.mag_item>(sqlItem, new { id });

            if (item == null)
            {
                _logger.LogWarning($"Item with ID {id} not found.");
                return null;
            }

  
            var sqlCollectionIds = new SqlQueryBuider("mag", "PostgreSql")
                .Select("collection_id")
                .From("mag_collection_item")
                .Where("item_id = @item_id")
                .Build();

            _logger.LogInformation($"Generated SQL Query for collection IDs: {sqlCollectionIds}");
            var collectionIds = await _unitOfWork.Repository.QueryListAsync<Guid>(sqlCollectionIds, new { item_id = id });

            var sqlCollections = new SqlQueryBuider("mag", "PostgreSql")
                .Select("id, name")
                .From("mag_collection")
                .Where("id = ANY(@collection_ids)")
                .Build();

            _logger.LogInformation($"Generated SQL Query for collections: {sqlCollections}");
            var collections = await _unitOfWork.Repository.QueryListAsync<dynamic>(sqlCollections, new { collection_ids = collectionIds.ToArray() });

            return new
            {
                Item = item,
                Collections = collections
            };
        }


        public async Task<Entities.mag_item> CreateItemAsync(Entities.mag_item item, List<Guid> collectionIds)
        {
            item.id = item.id == Guid.Empty ? Guid.NewGuid() : item.id;


            var insertItemQuery = new SqlQueryBuider("mag", "PostgreSql")
                .InsertInto("mag_item", new List<string> { "id", "title", "description", "order_index", "avatar", "path", "tenant" })
                .Build();

            await _unitOfWork.Repository.ExecuteAsync(insertItemQuery, new
            {
                id = item.id,
                title = item.title,
                description = item.description,
                order_index = item.order_index,
                avatar = item.avatar,
                path = item.path,
                tenant = item.tenant
            });


            foreach (var collectionId in collectionIds)
            {
                var insertCollectionItemQuery = new SqlQueryBuider("mag", "PostgreSql")
                    .InsertInto("mag_collection_item", new List<string> { "id", "collection_id", "item_id", "tenant" })
                    .Build();

                await _unitOfWork.Repository.ExecuteAsync(insertCollectionItemQuery, new
                {
                    id = Guid.NewGuid(),
                    collection_id = collectionId,
                    item_id = item.id,
                    tenant = item.tenant
                });
            }

            return item;
        }


        public async Task<Entities.mag_item> UpdateItemAsync(Guid id, Entities.mag_item item, List<Guid> newCollectionIds)
        {

            var updateItemQuery = new SqlQueryBuider("mag", "PostgreSql")
                .Update("mag_item")
                .Set(new List<string> { "title", "description", "order_index", "avatar", "path", "tenant" })
                .Where("id = @id")
                .Build();

            await _unitOfWork.Repository.ExecuteAsync(updateItemQuery, new
            {
                id,
                title = item.title,
                description = item.description,
                order_index = item.order_index,
                avatar = item.avatar,
                path = item.path,
                tenant = item.tenant
            });

            var existingCollectionIdsQuery = new SqlQueryBuider("mag", "PostgreSql")
                .Select("collection_id")
                .From("mag_collection_item")
                .Where("item_id = @item_id")
                .Build();

            var existingCollectionIds = (await _unitOfWork.Repository.QueryListAsync<Guid>(existingCollectionIdsQuery, new { item_id = id })).ToList();


            var collectionsToAdd = newCollectionIds.Except(existingCollectionIds).ToList();


            var collectionsToRemove = existingCollectionIds.Except(newCollectionIds).ToList();

            if (collectionsToRemove.Any())
            {
                var deleteLinksQuery = new SqlQueryBuider("mag", "PostgreSql")
                    .DeleteFrom("mag_collection_item")
                    .Where("item_id = @item_id AND collection_id = ANY(@collection_ids)")
                    .Build();

                await _unitOfWork.Repository.ExecuteAsync(deleteLinksQuery, new
                {
                    item_id = id,
                    collection_ids = collectionsToRemove.ToArray()
                });
            }


            foreach (var collectionId in collectionsToAdd)
            {
                var insertCollectionItemQuery = new SqlQueryBuider("mag", "PostgreSql")
                    .InsertInto("mag_collection_item", new List<string> { "id", "collection_id", "item_id", "tenant" })
                    .Build();

                await _unitOfWork.Repository.ExecuteAsync(insertCollectionItemQuery, new
                {
                    id = Guid.NewGuid(),
                    collection_id = collectionId,
                    item_id = id,
                    tenant = item.tenant
                });
            }

            return item;
        }


        public async Task DeleteItemAsync(Guid id)
        {
            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                .DeleteFrom("mag_item")
                .Where("id = @id");

            var sql = sqlQueryBuider.Build();
            _logger.LogInformation($"Generated SQL Query: {sql}");

            await _unitOfWork.Repository.ExecuteAsync(sql, new { id });
        }

        public async Task<IEnumerable<mag_collection_item>> GetCollectionItemsByItemIdAsync(Guid itemId)
        {
            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                .Select("*")
                .From("mag_collection_item")
                .Where("item_id = @itemId");

            var sql = sqlQueryBuider.Build();
            _logger.LogInformation($"Generated SQL Query: {sql}");

            return await _unitOfWork.Repository.QueryListAsync<mag_collection_item>(sql, new { itemId });
        }

    }
}
