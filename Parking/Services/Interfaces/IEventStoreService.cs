using Parking.Utilities.DTOs;

namespace Parking.Services.Interfaces
{
    public interface IEventStoreService
    {
        void CreateEvent(CreateEventDto dto, CancellationToken cancellationToken);
    }
}
