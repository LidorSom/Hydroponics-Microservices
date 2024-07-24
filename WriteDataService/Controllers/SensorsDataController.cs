using Microsoft.AspNetCore.Mvc;
using HydroponicsService.Models;
using HydroponicsService.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Serilog;
using ILogger = Serilog.ILogger;

namespace HydroponicsService.Controllers
{
    [ApiController]
    [Route("api/sensor-data")]
    public class SensorDataController : ControllerBase
    {
        private readonly ISensorDataService _sensorDataService;
        private readonly ILogger _logger;

        public SensorDataController(ISensorDataService sensorDataService, ILogger logger)
        {
            _sensorDataService = sensorDataService;
            _logger = logger;
        }

        [HttpGet("latest")]
        public async Task<ActionResult<SensorData>> GetLatestSensorData()
        {
            _logger.Information("Fetching latest sensor data");
            try
            {
                var sensorData = await _sensorDataService.GetLatestSensorsDataAsync();
                _logger.Information("Latest sensor data fetched successfully");
                return Ok(sensorData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while fetching the latest sensor data");
                return StatusCode(500, $"An error occurred while fetching the latest sensor data: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveSensorData([FromBody][Required] SensorData sensorData)
        {
            _logger.Information("Saving sensor data");
            try
            {
                await _sensorDataService.SaveSensorsDataAsync(sensorData);
                _logger.Information("Sensor data saved successfully");
                return CreatedAtAction(nameof(SaveSensorData), new { id = sensorData.Id }, sensorData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while saving sensor data");
                return StatusCode(500, $"An error occurred while saving sensor data: {ex.Message}");
            }
        }

        [HttpGet("by-timestamp")]
        public async Task<ActionResult<SensorData>> GetSensorDataByTimestamp([FromQuery][Required] DateTime timestamp)
        {
            _logger.Information("Fetching sensor data by timestamp: {Timestamp}", timestamp);
            try
            {
                var sensorData = await _sensorDataService.GetSensorsDataByTimestampAsync(timestamp);
                if (sensorData == null)
                {
                    _logger.Information("No sensor data found for timestamp: {Timestamp}", timestamp);
                    return NotFound();
                }
                _logger.Information("Sensor data fetched successfully for timestamp: {Timestamp}", timestamp);
                return Ok(sensorData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while fetching sensor data by timestamp: {Timestamp}", timestamp);
                return Problem($"An error occurred while fetching sensor data by timestamp: {ex.Message}");
            }
        }

        [HttpGet("by-timerange")]
        public ActionResult<IAsyncEnumerable<SensorData>> GetSensorDataByTimeRange(
            [FromQuery][Required] DateTime startTime,
            [FromQuery][Required] DateTime endTime,
            CancellationToken cancellationToken)
        {
            _logger.Information("Fetching sensor data by time range: {StartTime} to {EndTime}", startTime, endTime);
            try
            {
                var sensorDataStream = _sensorDataService.GetSensorsDataByTimeRangeAsync(startTime, endTime, cancellationToken);
                _logger.Information("Sensor data stream created successfully for time range: {StartTime} to {EndTime}", startTime, endTime);
                return Ok(sensorDataStream);
            }
            catch (OperationCanceledException)
            {
                _logger.Warning("The request was canceled for time range: {StartTime} to {EndTime}", startTime, endTime);
                return Problem("The request was canceled", statusCode: 499);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while fetching sensor data by time range: {StartTime} to {EndTime}", startTime, endTime);
                return Problem($"An error occurred while fetching sensor data by time range: {ex.Message}");
            }
        }
    }
}
