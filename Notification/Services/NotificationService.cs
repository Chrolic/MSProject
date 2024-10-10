using Notification.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using RestSharp;
using EventStore.Utilities.DTOs;

namespace Notification.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private static readonly HttpClient _client = new HttpClient();

        public NotificationService(IEmailService emailService, ISmsService smsService)
        {
            _emailService = emailService;
            _smsService = smsService;
        }

        public void TestEmail(string email, CancellationToken cancellationToken)
        {
            _emailService.SendTestEmail(email, cancellationToken);
        }

        public void TestSms(string number, CancellationToken cancellationToken)
        {
            _smsService.SendTestSms(number, cancellationToken);
        }
    }
}
