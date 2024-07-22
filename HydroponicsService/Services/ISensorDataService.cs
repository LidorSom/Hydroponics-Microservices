using HydroponicsService.Models;

namespace HydroponicsService.Services
{
    public interface ISensorDataService
    {
        Task<SensorData> GetLatestSensorsDataAsync();
        Task SaveSensorsDataAsync(SensorData sensorData);
        Task<SensorData> GetSensorsDataByTimestampAsync(DateTime timestamp);
        IAsyncEnumerable<SensorData> GetSensorsDataByTimeRangeAsync(
                                        DateTime startTime,
                                        DateTime endTime,
                                        CancellationToken cancellationToken = default);
    }
}
