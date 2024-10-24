using Microsoft.AspNetCore.Mvc;
using Moq;
using Pizzaria.Controllers;
using Pizzaria.Services.Interfaces;
using Pizzaria.Utilities.DTOs;

namespace UnitTests.UnitTests.Pizzaria
{
    public class PizzariaTests
    {
        private Mock<IPizzariaService> _pizzariaService;

        public PizzariaTests()
        {
            _pizzariaService = new Mock<IPizzariaService>();
        }

        [Fact]
        public async void TestPizzariaController_PlaceOrder()
        {
            // Test pizzaria controller response using Nuget 'Moq'.

            // ------- ARRANGE -------
            var cancellationTokenSource = new CancellationTokenSource();
            var pizzariaController = new PizzariaController(_pizzariaService.Object);

            var createOrderDto = new CreateOrderDto
            {
                CustomerName = "Carsten Carstensen",
                Address = "Generisk vej 1",
                PhoneNumber = "12345678",
                Table = "12B",
                PizzaMenuNumber = 9
            };

            var returnDto = new ReadEventDto
            {
                SequenceNumber = 1,
                OccuredAt = DateTimeOffset.UtcNow,
                Name = createOrderDto.CustomerName,
                Content = new
                {
                    CustomerName = createOrderDto.CustomerName,
                    Address = createOrderDto.Address,
                    PhoneNumber = createOrderDto.PhoneNumber,
                    Table = createOrderDto.Table,
                    PizzaMenuNumber = createOrderDto.PizzaMenuNumber
                }
            };

            _pizzariaService.Setup(x => x.PlaceOrder(createOrderDto, cancellationTokenSource.Token)).ReturnsAsync(returnDto);


            // ------- ACT -------
            var result = await pizzariaController.PlaceOrder(createOrderDto, cancellationTokenSource.Token);

            // Create result object to assert on
            var resultObject = result as OkObjectResult;


            // ------- ASSERT -------
            Assert.NotNull(resultObject);
            Assert.IsType<OkObjectResult>(resultObject);
            Assert.Equal(200, resultObject.StatusCode);
            Assert.Equal(returnDto, resultObject.Value);
        }
    }
}