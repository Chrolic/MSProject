using Microsoft.AspNetCore.Mvc;
using Parking.Services.Interfaces;
using Parking.Utilities.DTOs;

namespace Parking.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    [Produces("application/json")]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }


        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult RegisterCarParkingStart([FromBody] CarParkingDto dto, CancellationToken cancellationToken)
        {
            _parkingService.RegisterCarParkingStart(dto, cancellationToken);
            return new OkResult();
        }

        [HttpPut]
        [ProducesResponseType(204)]
        public IActionResult RegisterCarParkingEnd([FromBody] CarParkingDto dto)
        {
            _parkingService.RegisterCarParkingEnd(dto);
            return new NoContentResult();
        }

        [HttpPost]
        [ProducesResponseType(typeof(ParkingStatusDto), 200)]
        public IActionResult CheckCarParkingStatus([FromBody] string registrationNumber)
        {
            var result = _parkingService.CarParkingStatus(registrationNumber);
            return new OkObjectResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ParkingLocationDto), 200)]
        public IActionResult CheckCarParkingLocation([FromBody] ParkingLocationDto dto)
        {
            var result = _parkingService.CheckLocationForCar(dto);
            return new OkObjectResult(result);
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        public IActionResult DeleteParkingInfo([FromBody] string registrationNumber)
        {
            _parkingService.DeleteParkingInformation(registrationNumber);
            return new OkResult();
        }
    }
}
