using Pizzaria.Utilities.DTOs;
using RestSharp;

namespace Pizzaria.Services.Interfaces
{
    public interface IEventStoreService
    {
        Task<RestResponse> CreateEvent(CreateEventDto dto, CancellationToken cancellationToken);
    }
}
