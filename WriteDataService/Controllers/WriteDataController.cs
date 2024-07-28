using Microsoft.AspNetCore.Mvc;
using SharedLibrary;
using WriteDataService.Models;
using WriteDataService.Services;

namespace WriteDataService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WriteDataController : ControllerBase
    {
        private readonly ILogger<WriteDataController> _logger;
        private readonly IDataService _dataService;
        private readonly IMappingService _mappingService;

        public WriteDataController(ILogger<WriteDataController> logger, IDataService dataService, IMappingService mappingService)
        {
            _logger = logger;
            _dataService = dataService;
            _mappingService = mappingService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SensorDataBaseWriteDto sensorDataDto)
        {
            try
            {
                var sensorDataMongo = _mappingService.MapToSensorDataMongo(sensorDataDto);
                await _dataService.InsertSensorDataAsync(sensorDataMongo);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting sensor data");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestSensorData()
        {
            try
            {
                var latestSensorData = await _dataService.GetLatestSensorDataAsync();
                return Ok(latestSensorData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving latest sensor data");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensorDataById(string id)
        {
            try
            {
                var sensorData = await _dataService.GetSensorDataByIdAsync(id);
                if (sensorData == null)
                {
                    return NotFound();
                }
                return Ok(sensorData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving sensor data with ID: {id}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensorDataById(string id)
        {
            try
            {
                var result = await _dataService.DeleteSensorDataByIdAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting sensor data with ID: {id}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
