using Pizzaria.Utilities.DTOs;

namespace Pizzaria.Services.Interfaces
{
    public interface IPizzariaService
    {
        Task<ReadEventDto> PlaceOrder(CreateOrderDto dto, CancellationToken cancellationToken);
    }
}
