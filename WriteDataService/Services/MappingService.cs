using AutoMapper;

using SharedLibrary;

using WriteDataMicroservice.Intefaces;

namespace WriteDataMicroservice.Services
{
    public class MappingService : IMappingService
    {
        private readonly IMapper _mapper;

        public MappingService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public SensorDataBase MapToSensorDataBase(SensorDataBaseWriteDTO sensorDataDto)
        {
            return _mapper.Map<SensorDataBase>(sensorDataDto);
        }

        public SensorDataMongo MapToSensorDataMongo(SensorDataBase sensorDataBase)
        {
            return _mapper.Map<SensorDataMongo>(sensorDataBase);
        }
    }
}
