namespace Parking.Utilities.DTOs
{
    public class ParkingLocationDto
    {
        public required string RegistrationNumber { get; set; }
        public ParkingSpotDto ParkingSpot { get; set; }
        public bool CarOnLocation { get; set; }
    }
}
