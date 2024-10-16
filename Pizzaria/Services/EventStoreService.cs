﻿using Pizzaria.Services.Interfaces;
using Pizzaria.Utilities.DTOs;
using RestSharp;

namespace Pizzaria.Services
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IConfiguration _configuration;

        public EventStoreService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<RestResponse> CreateEvent(CreateEventDto dto, CancellationToken cancellationToken)
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

            return response;
        }
    }
}