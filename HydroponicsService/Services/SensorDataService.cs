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
            // TODO: Implement logic to fetch the latest sensor data from MongoDB
            return await _sensorDataCollection
                .Find(_ => true)
                .SortByDescending(s => s.Timestamp)
                .FirstOrDefaultAsync();
        }

        public async Task SaveSensorDataAsync(SensorData sensorData)
        {
            // TODO: Implement logic to save sensor data to MongoDB
            sensorData.Timestamp = DateTime.UtcNow;
            await _sensorDataCollection.InsertOneAsync(sensorData);
        }
    }
}
