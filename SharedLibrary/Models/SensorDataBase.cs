using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SharedLibrary;
public class SensorDataBase
{
    /// <summary>
    /// Id - the Id per measuring system of a user (user can have many measuing systems)
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    [Required]
    [Range(0, 14)]
    public double Ph { get; set; }

    [Required]
    [Range(0, 5000)]
    public double Ec { get; set; }

    [Required]
    public DateTime Timestamp { get; set; }

    [Range(-50, 50)]
    public double? WaterTemperature { get; set; }
    [Range(0, 100)]
    public double? Humidity { get; set; }
}
