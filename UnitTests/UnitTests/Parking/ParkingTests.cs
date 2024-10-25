using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking.Controllers;
using Parking.Services.Interfaces;
using Parking.Utilities.DTOs;

namespace UnitTests.UnitTests.Parking
{
    public class ParkingTests
    {
        private Mock<IParkingService> _parkingService;

        public ParkingTests()
        {
            _parkingService = new Mock<IParkingService>();
        }


        [Fact]
        public void TestParkingController_RegisterCarParkingStart()
        {
            // Test controller endpoint using Nuget "Moq".
            // Note: This only tests the controller, not the underlaying businesslogic in services.

            // ------- ARRANGE -------
            var cancellationTokenSource = new CancellationTokenSource();
            var parkingController = new ParkingController(_parkingService.Object);

            var inputDto = new CarParkingDto
            {
                RegistrationNumber = "DR24465",
                TimeOfParkingStart = DateTimeOffset.UtcNow,
                TelephoneNumber = "+4542395827",
                Email = "test@test.dk",
                ParkingSpot = new ParkingSpotDto
                {
                    Lot = "B",
                    Row = "4",
                    Plot = "17"
                }
            };

            var returnDtoCarParkingStatus = new ParkingStatusDto
            {
                RegistrationNumber = inputDto.RegistrationNumber,
                TimeOfParkingStart = inputDto.TimeOfParkingStart,
                TimeOfParkingEnd = null,
                ParkingActive = true
            };

            _parkingService.Setup(x => x.RegisterCarParkingStart(inputDto));
            _parkingService.Setup(x => x.CarParkingStatus(inputDto.RegistrationNumber)).Returns(returnDtoCarParkingStatus);


            // ------- ACT -------
            var resultParkingStart = parkingController.RegisterCarParkingStart(inputDto);
            var resultParkingStatus = parkingController.CheckCarParkingStatus(inputDto.RegistrationNumber);

            // Create result object to assert on
            var resultObjectParkingStart = resultParkingStart as OkResult;
            var resultObjectParkingStatus = resultParkingStatus as OkObjectResult;


            // ------- ASSERT -------
            // Check RegisterCarParkingStart
            Assert.NotNull(resultObjectParkingStart);
            Assert.IsType<OkResult>(resultObjectParkingStart);
            Assert.Equal(200, resultObjectParkingStart.StatusCode);

            // Check CheckCarParkingStatus
            Assert.NotNull(resultObjectParkingStatus);
            Assert.IsType<OkObjectResult>(resultObjectParkingStatus);
            Assert.Equal(200, resultObjectParkingStatus.StatusCode);
            Assert.Equal(returnDtoCarParkingStatus, resultObjectParkingStatus.Value);
        }

        [Fact]
        public void TestParkingController_RegisterCarParkingEnd()
        {
            // Test controller endpoint using Nuget "Moq".
            // Note: This only tests the controller, not the underlaying businesslogic in services.

            // ------- ARRANGE -------
            var cancellationTokenSource = new CancellationTokenSource();
            var parkingController = new ParkingController(_parkingService.Object);

            var inputDto = new CarParkingDto
            {
                RegistrationNumber = "DR24465",
                TimeOfParkingStart = DateTimeOffset.UtcNow.AddHours(-1),
                TimeOfParkingEnd = DateTimeOffset.UtcNow,
                TelephoneNumber = "+4542395827",
                Email = "test@test.dk",
                ParkingSpot = new ParkingSpotDto
                {
                    Lot = "B",
                    Row = "4",
                    Plot = "17"
                }
            };

            var returnDtoCarParkingStatus = new ParkingStatusDto
            {
                RegistrationNumber = inputDto.RegistrationNumber,
                TimeOfParkingStart = inputDto.TimeOfParkingStart,
                TimeOfParkingEnd = inputDto.TimeOfParkingEnd,
                ParkingActive = false
            };

            _parkingService.Setup(x => x.RegisterCarParkingEnd(inputDto));
            _parkingService.Setup(x => x.CarParkingStatus(inputDto.RegistrationNumber)).Returns(returnDtoCarParkingStatus);


            // ------- ACT -------
            var resultParkingEnd = parkingController.RegisterCarParkingEnd(inputDto);
            var resultParkingStatus = parkingController.CheckCarParkingStatus(inputDto.RegistrationNumber);

            // Create result object to assert on
            var resultObjectParkingEnd = resultParkingEnd as NoContentResult;
            var resultObjectParkingStatus = resultParkingStatus as OkObjectResult;


            // ------- ASSERT -------
            // Check RegisterCarParkingStart
            Assert.NotNull(resultObjectParkingEnd);
            Assert.IsType<NoContentResult>(resultObjectParkingEnd);
            Assert.Equal(204, resultObjectParkingEnd.StatusCode);

            // Check CheckCarParkingStatus
            Assert.NotNull(resultObjectParkingStatus);
            Assert.IsType<OkObjectResult>(resultObjectParkingStatus);
            Assert.Equal(200, resultObjectParkingStatus.StatusCode);
            Assert.Equal(returnDtoCarParkingStatus, resultObjectParkingStatus.Value);
        }
    }
}
