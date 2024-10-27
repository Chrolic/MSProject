
using EventStore.Utilities.DTOs;

namespace Notification.Services.Interfaces
{
    public interface INotificationService
    {
        void SendNotificationForNewCarParkingRegistrations(CancellationToken cancellationToken);
        void SendEmail(string email, CancellationToken cancellationToken);
        void SendSms(string number, CancellationToken cancellationToken);
    }
}
