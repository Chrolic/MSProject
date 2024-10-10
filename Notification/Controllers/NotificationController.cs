using EventStore.Utilities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Notification.Services.Interfaces;

namespace Notification.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    [Produces("application/json")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult TestEmail([FromBody] string emailAddress)
        {
            _notificationService.TestEmail(emailAddress, HttpContext.RequestAborted);
            return new OkResult();
        }

        [HttpPut]
        [ProducesResponseType(204)]
        public IActionResult TestSms([FromBody] string phoneNumber)
        {
            _notificationService.TestSms(phoneNumber, HttpContext.RequestAborted);
            return new OkResult();
        }
    }
}
