using Parking.Utilities.DTOs;

namespace Parking.Services.Interfaces
{
    public interface IParkingService
    {
        ParkingStatusDto CarParkingStatus(string registrationNumber);
        bool CheckLocationForCar(ParkingLocationDto dto);
        void DeleteParkingInformation(string registrationNumber);
        void RegisterCarParkingEnd(CarParkingDto carParking);
        void RegisterCarParkingStart(CarParkingDto carParking);
    }
}
