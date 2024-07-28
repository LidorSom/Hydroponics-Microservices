namespace ReadDataService;
using SharedLibrary;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

public class SensorsReadDataService
{
    private readonly IMongoCollection<SensorDataBase> _sensorDataCollection;

    public SensorsReadDataService(IMongoDatabase database)
    { // TO-DO - TO CHANGE HARD CODED COLLECTION NAME
        _sensorDataCollection = database.GetCollection<SensorDataBase>("SensorData");
    }
    public async Task<SensorDataBase> GetLatestSystemSingleDataAsync(Guid id)
    {
        return await _sensorDataCollection
            .Find(data => data.Id.Equals(id))
            .SortByDescending(s => s.Timestamp)
            .FirstOrDefaultAsync();
    }

    public async Task<SensorDataBase> GetSystemSingleDataByTimestampAsync(DateTime timestamp)
    {
        return await _sensorDataCollection
            .Find(s => s.Timestamp == timestamp)
            .FirstOrDefaultAsync();
    }

    public async IAsyncEnumerable<SensorDataBase> GetSystemDataByTimeRangeAsync(DateTime startTime, DateTime endTime, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var cursor = await _sensorDataCollection
            .Find(s => s.Timestamp >= startTime && s.Timestamp <= endTime)
            .SortBy(s => s.Timestamp)
            .ToCursorAsync(cancellationToken);

        while (await cursor.MoveNextAsync(cancellationToken))
        {
            foreach (var document in cursor.Current)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return document;
            }
        }
    }
}
