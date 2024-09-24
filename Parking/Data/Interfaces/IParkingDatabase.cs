using Parking.Utilities.DTOs;

namespace Parking.Data.Interfaces
{
    public interface IParkingDatabase
    {
        CarParkingDto GetParking(string registrationNumber);
        void RemoveParking(string registrationNumber);
        void StoreParking(CarParkingDto dto);
    }
}
