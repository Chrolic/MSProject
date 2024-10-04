using EventStore.Services.Interfaces;
using Notification.Services.Interfaces;

namespace Notification.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly IEventService _eventStore;

        public NotificationService(IEmailService emailService, ISmsService smsService, IEventService eventStore)
        {
            _emailService = emailService;
            _smsService = smsService;
            _eventStore = eventStore;
        }

        public string TestEmail(string email)
        {
            var result = _emailService.SendTestEmail(email);
            _eventStore.Raise("TestEmailSent", new { email });
            return result;
        }

        public string TestSms(string number)
        {
            var result = _smsService.SendTestSms(number);
            _eventStore.Raise("TestSmsSent", new { number });
            return result;
        }
    }
}
