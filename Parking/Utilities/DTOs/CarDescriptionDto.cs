namespace Parking.Utilities.DTOs
{
    public class CarDescriptionDto
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Variant { get; set; }

        public static CarDescriptionDto NONE = new();
    }
}
