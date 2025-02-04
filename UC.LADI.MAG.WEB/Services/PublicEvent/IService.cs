using System.Collections.Generic;
using System.Threading.Tasks;
using UC.Core.Interfaces;
using UC.LADI.MAG.WEB.Dtos;

namespace UC.LADI.MAG.WEB.Services.PublicEvent
{
    public interface IService : IRepositoryBase<Guid, UC.LADI.MAG.WEB.Entities.mag_event>
    {
        Task<List<EventDto>> GetPublicEventsAsync();
    }
}
