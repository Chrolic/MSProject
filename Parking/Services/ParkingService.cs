using Parking.Data.Interfaces;
using Parking.Services.Interfaces;
using Parking.Utilities.DTOs;

namespace Parking.Services
{
    public class ParkingService : IParkingService
    {
        private readonly IParkingDatabase _parkingDatabase;

        public ParkingService(IParkingDatabase parkingDatabase)
        {
            _parkingDatabase = parkingDatabase;
        }


        public void RegisterCarParkingStart(CarParkingDto carParking)
        {
            // Do some validity check

            // The correct thing would be to convert to a db model before passing on to data layer.
            _parkingDatabase.StoreParking(carParking);
        }

        public void RegisterCarParkingEnd(CarParkingDto dto)
        {
            // Do some validity check
            if (dto.TimeOfParkingEnd == null)
            {
                throw new Exception("No end time for parking.");
            }
            if (dto.RegistrationNumber == null)
            {
                throw new Exception("No registration number.");
            }

            // Since we allow for only reg and end time, we need to "update" the dic entry, by retriving the existing updating the object,
            // Removing the old entry, as the key already exist, and store the object again.
            var existingParking = _parkingDatabase.GetParking(dto.RegistrationNumber);

            if (existingParking == null)
            {
                throw new Exception("No parking registered.");
            }

            existingParking.TimeOfParkingEnd = dto.TimeOfParkingEnd;

            // The good ol' razzledazzle
            _parkingDatabase.RemoveParking(existingParking.RegistrationNumber);
            _parkingDatabase.StoreParking(existingParking);
        }

        public ParkingStatusDto CarParkingStatus(string registrationNumber)
        {
            // The correct thing would be to convert to a db model before passing on to data layer.
            var parking = _parkingDatabase.GetParking(registrationNumber);

            if (parking != null)
            {
                var psDto = new ParkingStatusDto();

                psDto.RegistrationNumber = registrationNumber;
                psDto.TimeOfParkingStart = parking.TimeOfParkingStart;

                if (parking.TimeOfParkingEnd != null)
                {
                    psDto.TimeOfParkingEnd = parking.TimeOfParkingEnd;
                    psDto.ParkingActive = false;

                    return psDto;
                }

                psDto.TimeOfParkingEnd = null;
                psDto.ParkingActive = true;
                return psDto;
            }

            return new ParkingStatusDto();
        }

        public bool CheckLocationForCar(ParkingLocationDto dto)
        {
            var parking = _parkingDatabase.GetParking(dto.RegistrationNumber);

            if (parking != null)
            {
                // Check to see if car is still parked
                var status = CarParkingStatus(dto.RegistrationNumber);
                if (status.ParkingActive == true)
                {
                    // Compare parking location
                    if (parking.ParkingSpot.Lot == dto.ParkingSpot.Lot && parking.ParkingSpot.Row == dto.ParkingSpot.Row && parking.ParkingSpot.Plot == dto.ParkingSpot.Plot)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void DeleteParkingInformation(string registrationNumber)
        {
            _parkingDatabase.RemoveParking(registrationNumber);
        }
    }
}
