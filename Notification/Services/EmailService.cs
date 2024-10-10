using Notification.Services.Interfaces;
using Notification.Utilities.DTOs;

namespace Notification.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEventStoreService _eventStoreService;

        public EmailService(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public void SendTestEmail(string email, CancellationToken cancellationToken)
        {
            // Create and send email.

            _eventStoreService.CreateEvent(new CreateEventDto
            {
                EventName = "Email was sent",
                Content = $"Email sent to: {email}"
            }, cancellationToken);
        }
    }
}
