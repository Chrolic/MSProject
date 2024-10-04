using Pizzaria.Services.Interfaces;
using Pizzaria.Utilities.DTOs;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using Pizzaria.Utilities.Exceptions;
using Pizzaria.Utilities.Models;
using System.Threading;

namespace Pizzaria.Services
{
    public class PizzariaService : IPizzariaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PizzariaService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }


        public async Task<ReadEventDto> PlaceOrder(CreateOrderDto dto, CancellationToken cancellationToken)
        {
            string url = $"http://localhost:5100/event/CreateEvent"; // move to appsettings, should be internal service route instead.

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestJson = ConvertNewPizzariaOrderToJson(dto);
                var result = await httpClient.PostAsJsonAsync(url, requestJson, cancellationToken);


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
