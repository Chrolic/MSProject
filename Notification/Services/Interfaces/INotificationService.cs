
using EventStore.Utilities.DTOs;

namespace Notification.Services.Interfaces
{
    public interface INotificationService
    {
        void TestEmail(string email, CancellationToken cancellationToken);
        void TestSms(string number, CancellationToken cancellationToken);
    }
}
