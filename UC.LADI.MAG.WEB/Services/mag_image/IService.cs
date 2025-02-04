using UC.Core.Interfaces;

namespace UC.LADI.MAG.WEB.Services.mag_image
{
    public interface IService : IRepositoryBase<Guid, Entities.mag_image>
    {
        Task<List<UC.LADI.MAG.WEB.Entities.mag_image>> GetAllImagesAsync();
        Task<UC.LADI.MAG.WEB.Entities.mag_image> GetImageByIdAsync(Guid id);
        Task<UC.LADI.MAG.WEB.Entities.mag_image> CreateImageAsync(UC.LADI.MAG.WEB.Entities.mag_image image);
        Task<UC.LADI.MAG.WEB.Entities.mag_image> UpdateImageAsync(Guid id, UC.LADI.MAG.WEB.Entities.mag_image image);
        Task DeleteImageAsync(Guid id);



    }

}
