using Microsoft.AspNetCore.Mvc;
using HydroponicsService.Models;
using HydroponicsService.Services;

namespace HydroponicsService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorDataController : ControllerBase
    {
        private readonly ISensorDataService _sensorDataService;

        public SensorDataController(ISensorDataService sensorDataService)
        {
            _sensorDataService = sensorDataService;
        }

        [HttpGet]
        public async Task<ActionResult<SensorData>> GetLatestSensorData()
        {
            var sensorData = await _sensorDataService.GetLatestSensorsDataAsync();
            return Ok(sensorData);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSensorData([FromBody] SensorData sensorData)
        {
            await _sensorDataService.SaveSensorsDataAsync(sensorData);
            return Ok();
        }
    }
}
