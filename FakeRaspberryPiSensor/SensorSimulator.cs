using System;
using SharedLibrary;

public class SensorSimulator
{
    private Random _random = new Random();

    public SensorDataBase GenerateSensorData()
    {
        return new SensorDataBase
        {
            Ph = GeneratePh(),
            Ec = GenerateEc(),
            WaterTemperature = GenerateTemperature(),
            Humidity = GenerateHumidity(),
            Timestamp = DateTime.UtcNow
        };
    }

    private double GeneratePh()
    {
        // pH typically ranges from 5.5 to 6.5 for hydroponics
        return _random.NextDouble() * (6.5 - 5.5) + 5.5;
    }

    private double GenerateEc()
    {
        // EC (Electrical Conductivity) typically ranges from 1.5 to 2.5 mS/cm for hydroponics
        return _random.NextDouble() * (2.5 - 1.5) + 1.5;
    }

    private double GenerateTemperature()
    {
        // Temperature typically ranges from 18 to 26°C for hydroponics
        return _random.NextDouble() * (26 - 18) + 18;
    }

    private double GenerateHumidity()
    {
        // Humidity typically ranges from 50% to 70% for hydroponics
        return _random.NextDouble() * (70 - 50) + 50;
    }
}
