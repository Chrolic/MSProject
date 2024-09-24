namespace Parking.Utilities.DTOs
{
    public class CarParkingDto
    {
        // Reg is unique, and to keep example simple will be used as dic id.
        public required string RegistrationNumber { get; set; }
        public DateTimeOffset TimeOfParkingStart { get; set; }
        public DateTimeOffset? TimeOfParkingEnd { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Email { get; set; }
        public ParkingSpotDto? ParkingSpot { get; set; }
    }
}
