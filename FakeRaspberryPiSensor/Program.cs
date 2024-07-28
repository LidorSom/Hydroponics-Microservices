using System;
using System.Threading.Tasks;
using SharedLibrary;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Fake Raspberry Pi Sensor Simulation Started");

        var sensorSimulator = new SensorSimulator();
        var sensorDataClient = new SensorDataClient("http://localhost:5000/api/sensordata"); // Adjust the URL as needed

        while (true)
        {
            var sensorData = sensorSimulator.GenerateSensorData();
            Console.WriteLine($"Timestamp: {sensorData.Timestamp}");
            Console.WriteLine($"pH: {sensorData.Ph:F2}");
            Console.WriteLine($"EC: {sensorData.Ec:F2}");
            Console.WriteLine($"Temperature: {sensorData.WaterTemperature:F2}°C");
            Console.WriteLine($"Humidity: {sensorData.Humidity:F2}%");
            Console.WriteLine("--------------------");

            await sensorDataClient.SendSensorDataAsync(sensorData);

            // Simulate real-time data by waiting for 5 seconds
            await Task.Delay(5000);
        }
    }
}
