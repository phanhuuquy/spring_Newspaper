using UC.Core.Interfaces;
using UC.LADI.MAG.WEB.Entities;

namespace UC.LADI.MAG.WEB.Services.mag_item
{
    public interface IService : IRepositoryBase<Guid, Entities.mag_item>
    {
        Task<List<object>> GetAllItemsAsync();
        Task<object> GetItemByIdAsync(Guid id);
        Task<Entities.mag_item> CreateItemAsync(Entities.mag_item item, List<Guid> collectionIds);
        Task<Entities.mag_item> UpdateItemAsync(Guid id, Entities.mag_item item, List<Guid> collectionIds);

        Task DeleteItemAsync(Guid id);
        Task<IEnumerable<mag_collection_item>> GetCollectionItemsByItemIdAsync(Guid itemId);
    }
}
