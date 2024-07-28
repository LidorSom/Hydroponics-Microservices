using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SharedLibrary.Models;

namespace WriteDataService.Services
{
    public class MongoDataService : IDataService
    {
        private readonly IMongoCollection<SensorDataMongo> _sensorDataCollection;
        private readonly ILogger<MongoDataService> _logger;

        public MongoDataService(ILogger<MongoDataService> logger, IMongoDatabase mongoDatabase)
        {
            _logger = logger;
            _sensorDataCollection = mongoDatabase.GetCollection<SensorDataMongo>("SensorData");
        }

        public async Task InsertSensorDataAsync(SensorDataMongo sensorData)
        {
            await _sensorDataCollection.InsertOneAsync(sensorData);
        }

        public async Task<SensorDataMongo> GetLatestSensorDataAsync()
        {
            var latestData = await _sensorDataCollection
                .Find(FilterDefinition<SensorDataMongo>.Empty)
                .SortByDescending(d => d.Timestamp)
                .FirstOrDefaultAsync();

            return latestData;
        }

        public async Task<SensorDataMongo> GetSensorDataByIdAsync(string id)
        {
            var filter = Builders<SensorDataMongo>.Filter.Eq(s => s.Id, id);
            var sensorData = await _sensorDataCollection.FindAsync(filter);
            return await sensorData.FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteSensorDataByIdAsync(string id)
        {
            var filter = Builders<SensorDataMongo>.Filter.Eq(s => s.Id, id);
            var result = await _sensorDataCollection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
