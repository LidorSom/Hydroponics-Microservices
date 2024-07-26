namespace SharedLibrary;
public class SensorDataBase
{
    /// <summary>
    /// Id - the Id per measuring system of a user (user can have many measuing systems)
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    public double Ph { get; set; }
    public double Ec { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public DateTime Timestamp { get; set; }
}
