
namespace Notification.Services.Interfaces
{
    public interface IEmailService
    {
        void SendTestEmail(string email, CancellationToken cancellationToken);
    }
}
