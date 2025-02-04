using Dapper;
using UC.Core.Interfaces;
using UC.LADI.MAG.WEB.Services.DbContext.master;
using UC.Core.Helpers.QueryBuilder;
using static Dapper.SqlMapper;
using System.Data.Common;
using UC.Core.Common;
using UC.Core.Helpers;

namespace UC.LADI.MAG.WEB.Services.mag_image
{
    public class Service : Repository<Guid, Entities.mag_image>, mag_image.IService
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

        public async Task<List<UC.LADI.MAG.WEB.Entities.mag_image>> GetAllImagesAsync()
        {
            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql");
            sqlQueryBuider
                .Select("*")
                .From("mag_image");
            var sql = sqlQueryBuider.Build();
            var image = await _unitOfWork.Repository.QueryListAsync<Entities.mag_image>(sql, null); 
            return image;
        }

        public async Task<UC.LADI.MAG.WEB.Entities.mag_image> GetImageByIdAsync(Guid id)
        {

            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                .Select("*") 
                .From("mag_image") 
                .Where("id = @id"); 
            var sql = sqlQueryBuider.Build();
            _logger.LogInformation($"Generated SQL Query: {sql}");


            var param = new { id };

            try
            {

                var image = await _unitOfWork.Repository.QueryFirstAsync<UC.LADI.MAG.WEB.Entities.mag_image>(sql, param);

                if (image == null)
                {
                    _logger.LogWarning($"Image with ID {id} not found.");
                }

                return image;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error executing query for ID {id}: {ex.Message}");
                throw new Exception("Error retrieving image by ID", ex);
            }
        }



        public async Task<UC.LADI.MAG.WEB.Entities.mag_image> CreateImageAsync(UC.LADI.MAG.WEB.Entities.mag_image image)
        {
            image.id = image.id == Guid.Empty ? Guid.NewGuid() : image.id;
            

            image.created_by = string.IsNullOrEmpty(_userProvider.UserName) ? "Guest" : _userProvider.UserName;
            image.created_datetime = _dateTimeProvider.OffsetNow;
            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                .InsertInto("mag_image", new List<string>
                {
                "id",
                "name",
                "path",
                "order_index",
                "event_id",
                "is_hot",
                "tenant",
                "created_by",
                "created_datetime"
                });

            var sql = sqlQueryBuider.Build();

            var param = new
            {
                id = image.id,
                name = image.name,
                path = image.path,
                order_index = image.order_index,
                event_id = image.event_id,
                is_hot = image.is_hot,
                tenant = image.tenant,
                created_by = image.created_by,
                created_datetime = image.created_datetime
            };
            _logger.LogInformation($"Generated SQL Query: {sql}");
            _logger.LogInformation($"Parameters: {Newtonsoft.Json.JsonConvert.SerializeObject(param)}");

            await _unitOfWork.Repository.ExecuteAsync(sql, param);

            return image;
        }

        public async Task<UC.LADI.MAG.WEB.Entities.mag_image> UpdateImageAsync(Guid id, UC.LADI.MAG.WEB.Entities.mag_image image)
        {
            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql");


            var updateColumns = new List<string>
                    {
                        "name", "path", "order_index", "event_id", "is_hot", "tenant", "updated_by", "updated_datetime"
                    };

            image.updated_by = string.IsNullOrEmpty(_userProvider.UserName) ? "Guest" : _userProvider.UserName;
            image.updated_datetime = _dateTimeProvider.OffsetNow;


            var param = new
            {
                id,
                image.name,
                image.path,
                image.order_index,
                image.event_id,
                image.is_hot,
                image.tenant,
                image.updated_by,
                image.updated_datetime
            };


            var sql = sqlQueryBuider
                .Update("mag_image") 
                .Set(updateColumns)
                .Where("id = @id") 
                .Build();


            _logger.LogInformation($"Generated SQL Query: {sql}");
            _logger.LogInformation($"Parameters: {Newtonsoft.Json.JsonConvert.SerializeObject(param)}");


            await _unitOfWork.Repository.ExecuteAsync(sql, param);

            return image;
        }



        public async Task DeleteImageAsync(Guid id)
        {
            SqlQueryBuider sqlQueryBuider = new SqlQueryBuider("mag", "PostgreSql")
                .DeleteFrom("mag_image")
                .Where("id = @id");

            var sql = sqlQueryBuider.Build();
            var param = new { id };
            await _unitOfWork.Repository.ExecuteAsync(sql, param);
        }
    }
} 
