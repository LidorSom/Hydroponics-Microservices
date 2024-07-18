using HydroponicsService.Models;

namespace HydroponicsService.Services
{
    public interface ISensorDataService
    {
        Task<SensorData> GetLatestSensorDataAsync();
        Task SaveSensorDataAsync(SensorData sensorData);
    }
}
