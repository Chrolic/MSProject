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
            var result = _notificationService.TestEmail(emailAddress);
            return new OkObjectResult(result);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        public IActionResult TestSms([FromBody] string phoneNumber)
        {
            var result = _notificationService.TestSms(phoneNumber);
            return new OkObjectResult(result);
        }
    }
}
