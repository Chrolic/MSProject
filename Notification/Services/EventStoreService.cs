using Notification.Services.Interfaces;
using Notification.Utilities.DTOs;
using RestSharp;
using System.Text.Json;

namespace Notification.Services
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IConfiguration _configuration;

        public EventStoreService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async void CreateEvent(CreateEventDto dto, CancellationToken cancellationToken)
        {
            // Create rest client
            var client = new RestClient(_configuration.GetValue<string>("EventStoreUrl"));

            // Create request
            var request = new RestRequest("/Event/CreateEvent", Method.Post);

            // Add json to request
            request.AddJsonBody(dto);

            // Add data to request
            request.RequestFormat = DataFormat.Json;

            // Execute the request
            var response = await client.ExecuteAsync(request, cancellationToken);
        }

        public async Task<RestResponse> GetEvents(long eventStart, long eventEnd, CancellationToken cancellationToken)
        {
            // Create rest client
            var client = new RestClient(_configuration.GetValue<string>("EventStoreUrl"));

            // Create request
            var request = new RestRequest("/Event/GetEvents", Method.Get);

            // Add paramters
            // Should be rewritten to be dynamic
            request.AddParameter("start", eventStart);
            request.AddParameter("end", eventEnd);

            // Add data to request
            request.RequestFormat = DataFormat.Json;

            // Execute the request
            var response = await client.ExecuteAsync(request, cancellationToken);
            return response;
        }

        public async Task<Stream> GetEventsAsStream(long eventStart, long eventEnd, CancellationToken cancellationToken)
        {
            // Create rest client
            var client = new RestClient(_configuration.GetValue<string>("EventStoreUrl"));

            // Create request
            var request = new RestRequest("/Event/GetEvents", Method.Get);

            // Add paramters
            // Should be rewritten to be dynamic
            request.AddParameter("start", eventStart);
            request.AddParameter("end", eventEnd);

            // Add data to request
            request.RequestFormat = DataFormat.Json;

            // Execute the request
            var response = await client.DownloadStreamAsync(request, cancellationToken);
            return response;
        }
    }
}
