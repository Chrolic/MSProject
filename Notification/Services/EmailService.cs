using Notification.Services.Interfaces;

namespace Notification.Services
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {

        }

        public string SendTestEmail(string email)
        {
            return "Hello from Notification service, email service";
        }
    }
}
