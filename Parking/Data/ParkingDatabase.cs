using Parking.Data.Interfaces;
using Parking.Utilities.DTOs;

namespace Parking.Data
{
    public class ParkingDatabase : IParkingDatabase
    {
        private static readonly Dictionary<string, CarParkingDto> _dbContext = new Dictionary<string, CarParkingDto>();

        public ParkingDatabase()
        {

        }

        public CarParkingDto GetParking(string registrationNumber)
        {
            return _dbContext[registrationNumber];
        }

        public void StoreParking(CarParkingDto dto)
        {
            _dbContext.Add(dto.RegistrationNumber, dto);
        }

        public void RemoveParking(string registrationNumber)
        {
            _dbContext.Remove(registrationNumber);
        }
    }
}
