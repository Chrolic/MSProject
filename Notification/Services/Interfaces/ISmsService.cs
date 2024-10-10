namespace Notification.Services.Interfaces
{
    public interface ISmsService
    {
        void SendTestSms(string number, CancellationToken cancellationToken);
    }
}
