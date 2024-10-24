using Pizzaria.Services.Interfaces;
using Pizzaria.Utilities.DTOs;

namespace Pizzaria.UnitTests.FakeServices
{
    public class FakePizzariaService : IPizzariaService
    {
        public async Task<ReadEventDto> PlaceOrder(CreateOrderDto dto, CancellationToken cancellationToken)
        {
            var returnDto = new ReadEventDto
            {
                SequenceNumber = 1,
                OccuredAt = DateTimeOffset.UtcNow,
                Name = dto.CustomerName,
                Content = new
                {
                    CustomerName = dto.CustomerName,
		            Address =  dto.Address,
		            PhoneNumber = dto.PhoneNumber,
		            Table = dto.Table,
		            PizzaMenuNumber = dto.PizzaMenuNumber
                }
            };
      
            return returnDto;
        }
    }
}
