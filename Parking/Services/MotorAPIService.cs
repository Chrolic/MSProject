using System.Net.Http.Headers;
using System.Net;
using Parking.Services.Interfaces;
using Parking.Utilities.DTOs;
using Newtonsoft.Json;
using Parking.Utilities.Exceptions;

namespace Parking.Services
{
    public class MotorAPIService : IMotorAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public MotorAPIService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<CarDescriptionDto> GetDescriptionAsync(string licensePlate)
        {
            string url = $"https://v1.motorapi.dk/vehicles/{licensePlate}";
            string key = _configuration.GetValue<string>("MotorApiKey");

            using (var c = _httpClientFactory.CreateClient())
            {
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                c.DefaultRequestHeaders.Add("x-auth-token", key);
                var result = await c.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync().Result;
                    CarTypeDto? car = JsonConvert.DeserializeObject<CarTypeDto>(content);

                    if (car != null)
                    {
                        return new()
                        {
                            Make = car.make,
                            Model = car.model,
                            Variant = car.variant
                        };
                    }
                    return CarDescriptionDto.NONE;

                }
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return CarDescriptionDto.NONE;
                }
                else
                {
                    throw new MotorApiException(result.StatusCode, result.Content);
                }
            }
        }
    }
}
