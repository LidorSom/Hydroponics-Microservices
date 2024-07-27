using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SharedLibrary;

public class SensorDataClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;

    public SensorDataClient(string apiUrl)
    {
        _httpClient = new HttpClient();
        _apiUrl = apiUrl;
    }

    public async Task SendSensorDataAsync(SensorDataBase sensorData)
    {
        var json = JsonSerializer.Serialize(sensorData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("Data sent successfully.");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error sending data: {e.Message}");
        }
    }
}
