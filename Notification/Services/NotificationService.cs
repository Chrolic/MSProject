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

        public string TestEmail(string email)
        {
            var result = _emailService.SendTestEmail(email);
            return result;
        }

        public string TestSms(string number)
        {
            var result = _smsService.SendTestSms(number);
            return result;
        }
    }
}
