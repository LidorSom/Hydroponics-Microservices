using HydroponicsService.Models;
using System.Runtime.CompilerServices;
using MongoDB.Driver;

namespace HydroponicsService.Services
{
    public class SensorDataService : ISensorDataService
    {
        private readonly IMongoCollection<SensorData> _sensorDataCollection;

        public SensorDataService(IMongoDatabase database)
        {
            _sensorDataCollection = database.GetCollection<SensorData>("SensorData");
        }

        public async Task<SensorData> GetLatestSensorsDataAsync()
        {
            return await _sensorDataCollection
                .Find(_ => true)
                .SortByDescending(s => s.Timestamp)
                .FirstOrDefaultAsync();
        }

        public async Task SaveSensorsDataAsync(SensorData sensorData)
        {
            sensorData.Timestamp = DateTime.UtcNow;
            await _sensorDataCollection.InsertOneAsync(sensorData);
        }

        public async Task<SensorData> GetSensorsDataByTimestampAsync(DateTime timestamp)
        {
            return await _sensorDataCollection
                .Find(s => s.Timestamp == timestamp)
                .FirstOrDefaultAsync();
        }

        public async IAsyncEnumerable<SensorData> GetSensorsDataByTimeRangeAsync(DateTime startTime, DateTime endTime, [EnumeratorCancellation] CancellationToken cancellationToken = default)
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
}
