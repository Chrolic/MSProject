using Notification.Services.Interfaces;

namespace Notification.Services
{
    public class SmsService : ISmsService
    {
        public SmsService()
        {
            
        }

        public string SendTestSms(string number)
        {
            return "Hello from Notification service, sms service";
        }
    }
}
