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
            try
            {
                var sensorData = await _sensorDataService.GetLatestSensorsDataAsync();
                return Ok(sensorData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the latest sensor data: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveSensorData([FromBody] SensorData sensorData)
        {
            try
            {
                await _sensorDataService.SaveSensorsDataAsync(sensorData);
                return CreatedAtAction(nameof(SaveSensorData), new { id = sensorData.Id }, sensorData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while saving sensor data: {ex.Message}");
            }
        }

        [HttpGet("by-timestamp")]
        public async Task<ActionResult<SensorData>> GetSensorDataByTimestamp([FromQuery] DateTime timestamp)
        {
            try
            {
                var sensorData = await _sensorDataService.GetSensorsDataByTimestampAsync(timestamp);
                if (sensorData == null)
                {
                    return NotFound();
                }
                return Ok(sensorData);
            }
            catch (Exception ex)
            {
                return Problem($"An error occurred while fetching sensor data by timestamp: {ex.Message}");
            }
        }

        [HttpGet("by-timerange")]
        public async Task<ActionResult<IEnumerable<SensorData>>> GetSensorDataByTimeRange(
            [FromQuery] DateTime startTime,
            [FromQuery] DateTime endTime,
            CancellationToken cancellationToken)
        {
            try
            {
                var sensorDataList = new List<SensorData>();
                await foreach (var sensorData in _sensorDataService.GetSensorsDataByTimeRangeAsync(startTime, endTime, cancellationToken))
                {
                    sensorDataList.Add(sensorData);
                }
                return Ok(sensorDataList);
            }
            catch (OperationCanceledException)
            {
                return Problem("The request was canceled", statusCode: 499);
            }
            catch (Exception ex)
            {
                return Problem($"An error occurred while fetching sensor data by time range: {ex.Message}");
            }
        }
    }
}
