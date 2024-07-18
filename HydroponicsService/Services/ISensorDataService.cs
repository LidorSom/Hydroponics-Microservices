using HydroponicsService.Models;

namespace HydroponicsService.Services
{
    public interface ISensorDataService
    {
        Task<SensorData> GetLatestSensorDataAsync();
        Task SaveSensorDataAsync(SensorData sensorData);
        Task<SensorData> GetSensorDataByTimestampAsync(DateTime timestamp);
        Task<IEnumerable<SensorData>> GetSensorDataByTimeRangeAsync(DateTime startTime, DateTime endTime);
    }
}
