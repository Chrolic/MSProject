using Notification.Utilities.DTOs;

namespace Notification.Services.Interfaces
{
    public interface IEventStoreService
    {
        void CreateEvent(CreateEventDto dto, CancellationToken cancellationToken);
    }
}
