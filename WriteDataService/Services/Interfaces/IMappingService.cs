namespace WriteDataMicroservice.Intefaces;
using SharedLibrary;

using WriteDataMicroservice;

internal interface IMappingService
{
    SensorDataBase MapToSensorDataBase(SensorDataBaseWriteDTO sensorDataDto);
    SensorDataMongo MapToSensorDataMongo(SensorDataBase sensorDataDto);
}