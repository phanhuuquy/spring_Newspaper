using System.Threading.Tasks;
using System.Collections.Generic;
using UC.Core.Helpers.QueryBuilder;
using UC.LADI.MAG.WEB.Dtos;
using UC.LADI.MAG.WEB.Entities;
using UC.LADI.MAG.WEB.Services.DbContext.master;
using Microsoft.Extensions.Logging;
using UC.Core.Interfaces;

namespace UC.LADI.MAG.WEB.Services.PublicEvent
{
    public class Service : Repository<Guid, Entities.mag_event>, IService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<ServiceWrapper> _logger;

        public Service(UnitOfWork unitOfWork, ILogger<ServiceWrapper> logger)
            : base(unitOfWork, null, null)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<EventDto>> GetPublicEventsAsync()
        {
            try
            {
                SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                     .Select("name, description, year, avatar")
                     .From("mag_event")
                     .OrderBy("year", "DESC")
                     .GetFirst(); 

                var sql = sqlQueryBuider.Build();
                _logger.LogInformation($"Generated SQL Query: {sql}");

                var events = await _unitOfWork.Repository.QueryListAsync<EventDto>(sql, null);
                return events;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching public events.");
                throw;
            }
        }
    }
}
