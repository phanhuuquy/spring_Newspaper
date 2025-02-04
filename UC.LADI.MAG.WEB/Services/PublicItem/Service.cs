using System.Collections.Generic;
using System.Threading.Tasks;
using UC.Core.Helpers.QueryBuilder;
using UC.LADI.MAG.WEB.Dtos;
using UC.LADI.MAG.WEB.Services.DbContext.master;
using Microsoft.Extensions.Logging;

namespace UC.LADI.MAG.WEB.Services.PublicItem
{
    public class Service : Repository<Guid, UC.LADI.MAG.WEB.Entities.mag_item>, IService
    {
        private readonly ILogger<ServiceWrapper> _logger;
        private readonly UnitOfWork _unitOfWork;

        public Service(UnitOfWork unitOfWork, ILogger<ServiceWrapper> logger)
            : base(unitOfWork, null, null)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<ItemDto>> GetTop10ItemsAsync()
        {
            try
            {
                SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                    .Select("id, title, description, avatar, path, order_index")
                    .From("mag_item")
                    .OrderBy("order_index", "ASC")
                    .Paging(1, 10);

                var sql = sqlQueryBuider.Build();
                _logger.LogInformation($"Generated SQL Query: {sql}");

                var items = await _unitOfWork.Repository.QueryListAsync<ItemDto>(sql, null);
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching top 10 items.");
                throw;
            }
        }

        public async Task<ItemDto> GetItemByIdAsync(Guid id)
        {
            try
            {
                SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                    .Select("id, title, description, avatar, path, order_index, tenant")
                    .From("mag_item")
                    .Where("id = @id");

                var sql = sqlQueryBuider.Build();
                _logger.LogInformation($"Generated SQL Query for GetItemById: {sql}");

                var item = await _unitOfWork.Repository.QueryFirstAsync<ItemDto>(sql, new { id });

                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching item with ID: {id}");
                throw;
            }
        }
    }
}
