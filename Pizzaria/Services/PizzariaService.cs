using Pizzaria.Services.Interfaces;
using Pizzaria.Utilities.DTOs;
using System.Text.Json;
using Pizzaria.Utilities.Models;
using RestSharp;
using Newtonsoft.Json;


namespace Pizzaria.Services
{
    public class PizzariaService : IPizzariaService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IEventStoreService _eventStoreService;

        public PizzariaService(IConfiguration configuration, IEventStoreService eventStoreService)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _eventStoreService = eventStoreService;
        }


        public async Task<ReadEventDto> PlaceOrder(CreateOrderDto dto, CancellationToken cancellationToken)
        {
            // Some pizzaria specific business logic here.

            // Log an event.
            var eventDto = CreateEventDto(dto);
            var response = await _eventStoreService.CreateEvent(eventDto, cancellationToken);

            return CreateReadEventDto(response);
        }


        private CreateEventDto CreateEventDto(CreateOrderDto dto)
        {
            return new CreateEventDto
            {
                EventName = "Pizzaria Order",
                Content = new Order
                {
                    Id = Guid.NewGuid(),
                    CustomerName = dto.CustomerName,
                    PhoneNumber = dto.PhoneNumber,
                    Address = dto.Address,
                    Table = dto.Table,
                    PizzaMenuNumber = dto.PizzaMenuNumber
                }
            };
        }

        private ReadEventDto CreateReadEventDto(RestResponse response)
        {
            return JsonConvert.DeserializeObject<ReadEventDto>(response.Content);
        }
    }
}
