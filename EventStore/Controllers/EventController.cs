using EventStore.Services.Interfaces;
using EventStore.Utilities.DTOs;
using EventStore.Utilities.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    [Produces("application/json")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }


        [HttpGet]
        public IActionResult GetEvents([FromQuery] long start, [FromQuery] long end = long.MaxValue)
        {
            if (start < 0 || end < start)
                return BadRequest();

            var result = _eventService.GetEvents(start, end);
            return new OkObjectResult(result);
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] CreateEventDto dto)
        {
            var result = _eventService.CreateEvent(dto);
            return new OkObjectResult(result);
        }

        [HttpDelete]
        public IActionResult DeleteEvent([FromBody] long seqNumber)
        {
            _eventService.DeleteEvent(seqNumber);
            return new OkResult();
        }
    }
}
