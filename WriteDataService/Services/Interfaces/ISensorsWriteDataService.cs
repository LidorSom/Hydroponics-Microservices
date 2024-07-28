
using SharedLibrary;

using WriteDataMicroservice;

namespace HydroponicsService.Services
{
    public interface ISensorsWriteDataService
    {
        Task SaveSystemDataSingleAsync(SensorDataBase sensorData);
    }
}
