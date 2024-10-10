﻿using Notification.Services.Interfaces;
using Notification.Utilities.DTOs;
using RestSharp;

namespace Notification.Services
{
    public class EventStoreService : IEventStoreService
    {
        public EventStoreService()
        {
            
        }

        public async void CreateEvent(CreateEventDto dto, CancellationToken cancellationToken)
        {
            // Create rest client
            var client = new RestClient("http://localhost:7000");

            // Create request
            var request = new RestRequest("/Event/CreateEvent", Method.Post);

            // Add json to request
            request.AddJsonBody(dto);

            // Add data to request
            request.RequestFormat = DataFormat.Json;

            // Execute the request
            var response = await client.ExecuteAsync(request, cancellationToken);
        }
    }
}