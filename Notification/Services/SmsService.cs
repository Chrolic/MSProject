using Notification.Services.Interfaces;
using Notification.Utilities.DTOs;
using System.Threading;

namespace Notification.Services
{
    public class SmsService : ISmsService
    {
        private readonly IEventStoreService _eventStoreService;

        public SmsService(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public void SendTestSms(string number, CancellationToken cancellationToken)
        {
            // Create and send sms

            _eventStoreService.CreateEvent(new CreateEventDto
            {
                EventName = "Sms was sent",
                Content = $"Sms sent to: {number}"
            }, cancellationToken);
        }
    }
}
