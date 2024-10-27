using Notification.Utilities.DTOs;
using RestSharp;

namespace Notification.Services.Interfaces
{
    public interface IEventStoreService
    {
        void CreateEvent(CreateEventDto dto, CancellationToken cancellationToken);
        Task<RestResponse> GetEvents(long eventStart, long eventEnd, CancellationToken cancellationToken);
        Task<Stream> GetEventsAsStream(long eventStart, long eventEnd, CancellationToken cancellationToken);
    }
}
