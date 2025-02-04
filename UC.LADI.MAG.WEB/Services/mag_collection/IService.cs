using UC.Core.Interfaces;

namespace UC.LADI.MAG.WEB.Services.mag_collection
{
    public interface IService : IRepositoryBase<Guid, Entities.mag_collection>
    {
        Task<List<Entities.mag_collection>> GetAllCollectionsAsync();
        Task<Entities.mag_collection> GetCollectionByIdAsync(Guid id );
        Task<Entities.mag_collection> CreateCollectionAsync(Entities.mag_collection collection);
        Task<Entities.mag_collection> UpdateCollectionAsync(Guid id, Entities.mag_collection collection);
        Task DeleteCollectionAsync(Guid id);

    }
}
