using System.Collections.Generic;
using System.Threading.Tasks;
using UC.LADI.MAG.WEB.Dtos;

namespace UC.LADI.MAG.WEB.Services.PublicItem
{
    public interface IService
    {
        Task<List<ItemDto>> GetTop10ItemsAsync();
        Task<ItemDto> GetItemByIdAsync(Guid id);
    }
}
