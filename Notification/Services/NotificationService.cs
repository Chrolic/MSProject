using Notification.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using RestSharp;
using System.Text.Json;
using Notification.Utilities.DTOs;
using System.Net.Http.Headers;
using System.IO;


namespace Notification.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly IEventStoreService _eventStoreService;
        private static readonly HttpClient _client = new HttpClient();

        public NotificationService(IEmailService emailService, ISmsService smsService, IEventStoreService eventStoreService)
        {
            _emailService = emailService;
            _smsService = smsService;
            _eventStoreService = eventStoreService;
        }


        public void SendNotificationForNewCarParkingRegistrations(CancellationToken cancellationToken)
        {
            SendCarParkingRegistrationNotification(cancellationToken);
        }

        public void SendEmail(string email, CancellationToken cancellationToken)
        {
            _emailService.SendTestEmail(email, cancellationToken);
        }

        public void SendSms(string number, CancellationToken cancellationToken)
        {
            _smsService.SendTestSms(number, cancellationToken);
        }


        private async void SendCarParkingRegistrationNotification(CancellationToken cancellationToken)
        {
            // This should be in a singleton or simelar.
            // Need to keep state of biggest/last read sequence number.
            // Implementation here for easy testing

            var result = await _eventStoreService.GetEvents(0, 10000, cancellationToken);
            var events = JsonConvert.DeserializeObject<ReadEventDto[]>(result.Content);

            foreach (var @event in events)
            {
                if (@event.Name == "Parking started")
                {
                    var dpDto = JsonConvert.DeserializeObject<CarParkingDto>(@event.Content.ToString());

                    SendEmail(dpDto.Email, cancellationToken);
                    SendSms(dpDto.TelephoneNumber, cancellationToken);
                }
            }
        }
    }
}
