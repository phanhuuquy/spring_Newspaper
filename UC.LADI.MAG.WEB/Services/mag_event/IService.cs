using UC.Core.Interfaces;

namespace UC.LADI.MAG.WEB.Services.mag_event
{
    public interface IService : IRepositoryBase<Guid, Entities.mag_event>
    {
        Task<List<Entities.mag_event>> GetAllEventsAsync();
        Task<Entities.mag_event> GetEventByIdAsync(Guid id);
        Task<Entities.mag_event> CreateEventAsync(Entities.mag_event eventItem);
        Task<Entities.mag_event> UpdateEventAsync(Guid id, Entities.mag_event eventItem);
        Task DeleteEventAsync(Guid id);
    }
}
