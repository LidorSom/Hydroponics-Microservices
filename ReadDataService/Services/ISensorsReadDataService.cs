using SharedLibrary;

namespace ReadDataService;

public interface ISensorsReadDataService
{
    Task<SensorDataBase> GetLatestSystemDataSingleAsync(Guid id);
    Task<SensorDataBase> GetSystemDataByTimestampSingleAsync(DateTime timestamp);
    IAsyncEnumerable<SensorDataBase> GetSystemDataByTimeRangeAsync(
                                    DateTime startTime,
                                    DateTime endTime,
                                    CancellationToken cancellationToken = default);
}
