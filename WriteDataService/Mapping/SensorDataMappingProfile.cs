using AutoMapper;

using SharedLibrary;

namespace WriteDataMicroservice.Mapping
{
    public class SensorDataMappingProfile : Profile
    {
        public SensorDataMappingProfile()
        {
            CreateMap<SensorDataBaseWriteDTO, SensorDataBase>();
            CreateMap<SensorDataBase, SensorDataMongo>();
        }
    }
}
