using Microsoft.AspNetCore.Mvc;
using HydroponicsService.Models;
using HydroponicsService.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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

        [HttpGet("latest")]
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

        [HttpGet("by-timestamp")]
        public async Task<ActionResult<SensorData>> GetSensorDataByTimestamp([FromQuery] DateTime timestamp)
        {
            var sensorData = await _sensorDataService.GetSensorsDataByTimestampAsync(timestamp);
            if (sensorData == null)
            {
                return NotFound();
            }
            return Ok(sensorData);
        }

        [HttpGet("by-timerange")]
        public async Task<ActionResult<IEnumerable<SensorData>>> GetSensorDataByTimeRange(
            [FromQuery] DateTime startTime,
            [FromQuery] DateTime endTime,
            CancellationToken cancellationToken)
        {
            var sensorDataList = new List<SensorData>();
            await foreach (var sensorData in _sensorDataService.GetSensorsDataByTimeRangeAsync(startTime, endTime, cancellationToken))
            {
                sensorDataList.Add(sensorData);
            }
            return Ok(sensorDataList);
        }
    }
}
