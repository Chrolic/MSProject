namespace Notification.Services.Interfaces
{
    public interface INotificationService
    {
        string TestEmail(string email);
        string TestSms(string number);
    }
}
