namespace Notification.Utilities.DTOs
{
    public class CarParkingDto
    {
        public required string RegistrationNumber { get; set; }
        public DateTimeOffset TimeOfParkingStart { get; set; }
        public DateTimeOffset? TimeOfParkingEnd { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
