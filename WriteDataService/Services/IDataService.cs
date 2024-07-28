using SharedLibrary.Models;

namespace WriteDataService.Services
{
    public interface IDataService
    {
        Task InsertSensorDataAsync(SensorDataMongo sensorData);
        Task<SensorDataMongo> GetLatestSensorDataAsync();
        Task<SensorDataMongo> GetSensorDataByIdAsync(string id);
        Task<bool> DeleteSensorDataByIdAsync(string id);
    }
}
