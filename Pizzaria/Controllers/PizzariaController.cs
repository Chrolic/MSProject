using Microsoft.AspNetCore.Mvc;
using Pizzaria.Services.Interfaces;
using Pizzaria.Utilities.DTOs;

namespace Pizzaria.Controllers
{

    [ApiController]
    [Route("[controller]/[Action]")]
    [Produces("application/json")]
    public class PizzariaController : ControllerBase
    {
        private readonly IPizzariaService _pizzariaService;

        public PizzariaController(IPizzariaService pizzariaService)
        {
            _pizzariaService = pizzariaService;
        }


        //[HttpGet]
        //public IActionResult GetOrders()
        //{
        //    var result = _pizzariaService.GetOrders();
        //    return new OkObjectResult(result);
        //}

        [HttpPost]
        public IActionResult PlaceOrder([FromBody] CreateOrderDto dto)
        {
            var result = _pizzariaService.PlaceOrder(dto, HttpContext.RequestAborted);
            return new OkObjectResult(result);
        }
    }
}
