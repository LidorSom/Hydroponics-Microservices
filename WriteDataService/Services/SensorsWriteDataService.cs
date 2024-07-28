using AutoMapper;

using MongoDB.Driver;

using SharedLibrary;

using WriteDataMicroservice;

namespace HydroponicsService.Services
{
    public class SensorWriteDataService : ISensorsWriteDataService
    {
        private readonly IMongoCollection<SensorDataMongo> _sensorDataCollection;
        private readonly IMapper _mapper;

        public SensorWriteDataService(IMongoDatabase database, IMapper mapper)
        {
            _sensorDataCollection = database.GetCollection<SensorDataMongo>("SensorData");
            _mapper = mapper;
        }

        public async Task SaveSystemDataSingleAsync(SensorDataBase sensorData)
        {
            var mappedData = _mapper.Map<SensorDataMongo>(sensorData);
            await _sensorDataCollection.InsertOneAsync(mappedData);
        }
    }
}
