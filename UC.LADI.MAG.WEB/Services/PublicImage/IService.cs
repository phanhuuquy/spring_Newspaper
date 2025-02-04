using System.Collections.Generic;
using System.Threading.Tasks;
using UC.Core.Interfaces;
using UC.LADI.MAG.WEB.Dtos;

namespace UC.LADI.MAG.WEB.Services.PublicImage
{
    public interface IService : IRepositoryBase<Guid, UC.LADI.MAG.WEB.Entities.mag_image>
    {
        Task<List<ImageDto>> GetAllItemsAsync();
    }
}
