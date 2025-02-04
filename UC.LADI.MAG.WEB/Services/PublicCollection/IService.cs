using System.Collections.Generic;
using System.Threading.Tasks;
using UC.LADI.MAG.WEB.Dtos;

namespace UC.LADI.MAG.WEB.Services.PublicCollection
{
    public interface IService
    {
        Task<List<CollectionDto>> GetAllCollectionsAsync();
        Task<CollectionWithItemsDto> GetCollectionWithItemsAsync(Guid collectionId);
    }
}
