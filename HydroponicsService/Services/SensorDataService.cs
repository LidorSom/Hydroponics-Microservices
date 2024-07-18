using HydroponicsService.Models;
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

        public async Task<SensorData> GetLatestSensorDataAsync()
        {
            return await _sensorDataCollection
                .Find(_ => true)
                .SortByDescending(s => s.Timestamp)
                .FirstOrDefaultAsync();
        }

        public async Task SaveSensorDataAsync(SensorData sensorData)
        {
            sensorData.Timestamp = DateTime.UtcNow;
            await _sensorDataCollection.InsertOneAsync(sensorData);
        }

        public async Task<SensorData> GetSensorDataByTimestampAsync(DateTime timestamp)
        {
            return await _sensorDataCollection
                .Find(s => s.Timestamp == timestamp)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SensorData>> GetSensorDataByTimeRangeAsync(DateTime startTime, DateTime endTime)
        {
            return await _sensorDataCollection
                .Find(s => s.Timestamp >= startTime && s.Timestamp <= endTime)
                .SortBy(s => s.Timestamp)
                .ToListAsync();
        }
    }
}
