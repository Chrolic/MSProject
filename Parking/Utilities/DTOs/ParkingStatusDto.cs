namespace Parking.Utilities.DTOs
{
    public class ParkingStatusDto
    {
        public string RegistrationNumber { get; set; }
        public DateTimeOffset TimeOfParkingStart { get; set; }
        public DateTimeOffset? TimeOfParkingEnd { get; set; }
        public bool ParkingActive { get; set; }
    }
}
