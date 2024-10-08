using Pizzaria.Services.Interfaces;
using Pizzaria.Utilities.DTOs;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using Pizzaria.Utilities.Exceptions;
using Pizzaria.Utilities.Models;


namespace Pizzaria.Services
{
    public class PizzariaService : IPizzariaService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PizzariaService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }


        public async Task<ReadEventDto> PlaceOrder(CreateOrderDto dto, CancellationToken cancellationToken)
        {
            string url = _configuration.GetValue<string>("EventStoreUrl") + "CreateEvent";
            var requestJson = ConvertNewPizzariaOrderToJson(dto);
            
            using (_httpClient)
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await _httpClient.PostAsJsonAsync(url, requestJson, cancellationToken);


                if (result.IsSuccessStatusCode)
                {
                    var eventDto = JsonSerializer.Deserialize<ReadEventDto>(result.Content.ReadAsStringAsync().Result);

                    if (eventDto != null)
                    {
                        return eventDto;
                    }

                }
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return ReadEventDto.NONE;
                }
                else
                {
                    throw new PizzariaException(result.StatusCode, result.Content);
                }
            }

        }


        private string ConvertNewPizzariaOrderToJson(CreateOrderDto dto)
        {

            return JsonSerializer.Serialize(new CreateEventDto
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
            });
        }
    }
}
