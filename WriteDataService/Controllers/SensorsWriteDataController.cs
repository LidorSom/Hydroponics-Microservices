using Microsoft.AspNetCore.Mvc;
using HydroponicsService.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Serilog;
using ILogger = Serilog.ILogger;
using WriteDataMicroservice;

namespace HydroponicsService.Controllers
{ // @@ TO CHECK WHETHER [REQUIRED] FIELDS MUST BE IN ORDER TO GET VALUE AS QUERY/BODY
    [ApiController]
    [Route("api/sensor-data")]
    public class SensorsWriteDataController : ControllerBase
    {
        private readonly ISensorsWriteDataService _sensorsWriteDataService;
        private readonly ILogger _logger;

        public SensorsWriteDataController(ISensorsWriteDataService sensorDataService, ILogger logger)
        {
            _sensorsWriteDataService = sensorDataService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SaveSensorData([FromBody] SensorDataBaseWriteDTO sensorDataDTO)
        {
            _logger.Information("Saving sensor data");
            try
            {
                await _sensorsWriteDataService.SaveSystemDataSingleAsync(sensorDataDTO);
                _logger.Information("Sensor data saved successfully");
                return CreatedAtAction(nameof(SaveSensorData), new { id = sensorDataDTO.Id }, sensorDataDTO);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while saving sensor data");
                return StatusCode(500, $"An error occurred while saving sensor data: {ex.Message}");
            }
        }
    }
}
