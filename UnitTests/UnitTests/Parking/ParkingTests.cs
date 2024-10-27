using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Parking.Controllers;
using Parking.Data.Interfaces;
using Parking.Services;
using Parking.Services.Interfaces;
using Parking.Utilities.DTOs;

namespace UnitTests.UnitTests.Parking
{
    public class ParkingTests
    {
        private Mock<IParkingService> _parkingService;
        private Mock<IParkingDatabase> _parkingDatabase;
        private Mock<IEventStoreService> _eventStoreService;

        public ParkingTests()

        {
            _parkingService = new Mock<IParkingService>();
            _parkingDatabase = new Mock<IParkingDatabase>();
            _eventStoreService = new Mock<IEventStoreService>();
        }


        [Fact]
        public void TestParkingController_RegisterCarParkingStart()
        {
            // Test controller endpoint using Nuget "Moq".
            // Note: This only tests the controller, not the underlaying businesslogic in services.

            // ------- ARRANGE -------
            var cancellationToken = new CancellationTokenSource().Token;
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

            _parkingService.Setup(x => x.RegisterCarParkingStart(inputDto, cancellationToken));
            _parkingService.Setup(x => x.CarParkingStatus(inputDto.RegistrationNumber)).Returns(returnDtoCarParkingStatus);


            // ------- ACT -------
            var resultParkingStart = parkingController.RegisterCarParkingStart(inputDto, cancellationToken);
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

        [Fact]
        public void TestParkingService_RegisterCarParkingEnd_NoRegistrationNumber_ExpectedException()
        {
            // Test controller endpoint using Nuget "Moq".
            // Note: This test against a validity check, and is expected to fail due to no registration sent in DTO.

            // ------- ARRANGE -------
            var parkingService = new ParkingService(_parkingDatabase.Object, _eventStoreService.Object);

            var inputDto = new CarParkingDto
            {
                RegistrationNumber = null,
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


            // ------- ACT -------
            Action act = () => parkingService.RegisterCarParkingEnd(inputDto);

            

            // ------- ASSERT -------
            Assert.Throws<Exception>(act);
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal("No registration number.", exception.Message);
        }

        [Fact]
        public void TestParkingService_RegisterCarParkingEnd_DateTimeEndBeforeStart_ExpectedException()
        {
            // Test controller endpoint using Nuget "Moq".
            // Note: This test against a validity check, and is expected to fail due to end time being before start time.

            // ------- ARRANGE -------
            var parkingService = new ParkingService(_parkingDatabase.Object, _eventStoreService.Object);

            var inputDto = new CarParkingDto
            {
                RegistrationNumber = "DR24465",
                TimeOfParkingStart = DateTimeOffset.UtcNow.AddHours(-1),
                TimeOfParkingEnd = DateTimeOffset.UtcNow.AddHours(-2),
                TelephoneNumber = "+4542395827",
                Email = "test@test.dk",
                ParkingSpot = new ParkingSpotDto
                {
                    Lot = "B",
                    Row = "4",
                    Plot = "17"
                }
            };


            // ------- ACT -------
            Action act = () => parkingService.RegisterCarParkingEnd(inputDto);



            // ------- ASSERT -------
            Assert.Throws<Exception>(act);
            Exception exception = Assert.Throws<Exception>(act);
            Assert.Equal("Parking set to end before start time.", exception.Message);
        }

        [Fact]
        public void TestParkingService_CarParkingStatus()
        {
            // Test controller endpoint using Nuget "Moq" & Fluent Assertion.
            // Note: This tests the business logic in the parking service.

            // ------- ARRANGE -------
            var parkingService = new ParkingService(_parkingDatabase.Object, _eventStoreService.Object);

            var cpDto = new CarParkingDto
            {
                RegistrationNumber = "DR24465",
                TimeOfParkingStart = DateTimeOffset.UtcNow.AddHours(-1),
                TimeOfParkingEnd = DateTimeOffset.UtcNow.AddMinutes(-1),
                TelephoneNumber = "+4542395827",
                Email = "test@test.dk",
                ParkingSpot = new ParkingSpotDto
                {
                    Lot = "B",
                    Row = "4",
                    Plot = "17"
                }
            };

            var expectedStatusDto = new ParkingStatusDto
            {
                RegistrationNumber = cpDto.RegistrationNumber,
                TimeOfParkingStart = cpDto.TimeOfParkingStart,
                TimeOfParkingEnd = cpDto.TimeOfParkingEnd,
                ParkingActive = false
            };

            _parkingDatabase.Setup(x => x.GetParking(cpDto.RegistrationNumber)).Returns(cpDto);

            // ------- ACT -------
            var result = parkingService.CarParkingStatus(cpDto.RegistrationNumber);


            // ------- ASSERT -------
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedStatusDto);
        }

        [Fact]
        public void TestParkingService_CheckLocationForCar_CorrectSpot()
        {
            // Test controller endpoint using Nuget "Moq" & Fluent Assertion.
            // Note: This tests the business logic in the parking service.
            // Check if the car is at the requested parking spot.

            // ------- ARRANGE -------
            var parkingService = new ParkingService(_parkingDatabase.Object, _eventStoreService.Object);

            var cpDto = new CarParkingDto
            {
                RegistrationNumber = "DR24465",
                TimeOfParkingStart = DateTimeOffset.UtcNow.AddHours(-1),
                TelephoneNumber = "+4542395827",
                Email = "test@test.dk",
                ParkingSpot = new ParkingSpotDto
                {
                    Lot = "B",
                    Row = "4",
                    Plot = "17"
                }
            };

            var plDto = new ParkingLocationDto
            {
                RegistrationNumber = cpDto.RegistrationNumber,
                ParkingSpot = new ParkingSpotDto
                {
                    Lot = "B",
                    Row = "4",
                    Plot = "17"
                }
            };

            _parkingDatabase.Setup(x => x.GetParking(cpDto.RegistrationNumber)).Returns(cpDto);


            // ------- ACT -------
            var result = parkingService.CheckLocationForCar(plDto);


            // ------- ASSERT -------
            result.Should().BeTrue();
        }

        [Fact]
        public void TestParkingService_CheckLocationForCar_WrongSpot()
        {
            // Test controller endpoint using Nuget "Moq" & Fluent Assertion.
            // Note: This tests the business logic in the parking service.
            // Check if the car is at the requested parking spot.

            // ------- ARRANGE -------
            var parkingService = new ParkingService(_parkingDatabase.Object, _eventStoreService.Object);

            var cpDto = new CarParkingDto
            {
                RegistrationNumber = "DR24465",
                TimeOfParkingStart = DateTimeOffset.UtcNow.AddHours(-1),
                TelephoneNumber = "+4542395827",
                Email = "test@test.dk",
                ParkingSpot = new ParkingSpotDto
                {
                    Lot = "B",
                    Row = "4",
                    Plot = "17"
                }
            };

            var plDto = new ParkingLocationDto
            {
                RegistrationNumber = cpDto.RegistrationNumber,
                ParkingSpot = new ParkingSpotDto
                {
                    Lot = "A",
                    Row = "2",
                    Plot = "9"
                }
            };

            _parkingDatabase.Setup(x => x.GetParking(cpDto.RegistrationNumber)).Returns(cpDto);

            // ------- ACT -------
            var result = parkingService.CheckLocationForCar(plDto);


            // ------- ASSERT -------
            result.Should().BeFalse();
        }
    }
}
