using System.Collections.Generic;
using System.Threading.Tasks;
using UC.Core.Helpers.QueryBuilder;
using UC.LADI.MAG.WEB.Dtos;
using UC.LADI.MAG.WEB.Entities;
using UC.LADI.MAG.WEB.Services.DbContext.master;
using Microsoft.Extensions.Logging;
using UC.Core.Interfaces;

namespace UC.LADI.MAG.WEB.Services.PublicImage
{
    public class Service : Repository<Guid, Entities.mag_image>, IService
    {
        private readonly ILogger<ServiceWrapper> _logger;
         private readonly UnitOfWork _unitOfWork;
        public Service(UnitOfWork unitOfWork, ILogger<ServiceWrapper> logger)
            : base(unitOfWork, null, null) 
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<ImageDto>> GetAllItemsAsync()
        {
            try
            {
                SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                    .Select("id, name, path, order_index, event_id, is_hot, tenant")
                    .From("mag_image")
                    .Where("is_hot = TRUE")
                    .OrderBy("order_index", "DESC")
                    .Paging(1, 4);

                var sql = sqlQueryBuider.Build();
                _logger.LogInformation($"Generated SQL Query: {sql}");

                var images = await _unitOfWork.Repository.QueryListAsync<ImageDto>(sql, null);
                return images;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching top hot images.");
                throw;
            }
        }
    }
}
