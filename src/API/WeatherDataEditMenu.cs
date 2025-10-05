using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.API;

public class WeatherDataEditMenu(IWeatherMonitoringService weatherService)
{
    private WeatherData? _currentWeatherData;

    public void SetCurrentWeatherData(WeatherData weatherData)
    {
        _currentWeatherData = weatherData;
    }

    public void ShowEditMenu()
    {
        if (_currentWeatherData == null)
        {
            Console.WriteLine("No weather data to edit.");
            return;
        }

        Console.WriteLine("\n--- Edit Weather Data ---");
        Console.WriteLine($"Location: {_currentWeatherData.Location}");
        Console.WriteLine($"Temperature: {_currentWeatherData.Temperature}°C");
        Console.WriteLine($"Humidity: {_currentWeatherData.Humidity}%");
        Console.WriteLine("\nEnter new values (press Enter to keep current value):");

        Console.Write($"New Temperature [{_currentWeatherData.Temperature}°C]: ");
        var tempInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(tempInput) && double.TryParse(tempInput, out var newTemperature))
        {
            _currentWeatherData = new WeatherData
            {
                Location = _currentWeatherData.Location,
                Temperature = newTemperature,
                Humidity = _currentWeatherData.Humidity
            };
            Console.WriteLine($"Temperature updated to {newTemperature}°C");
        }

        Console.Write($"New Humidity [{_currentWeatherData.Humidity}%]: ");
        var humidityInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(humidityInput) && double.TryParse(humidityInput, out var newHumidity))
        {
            _currentWeatherData = new WeatherData
            {
                Location = _currentWeatherData.Location,
                Temperature = _currentWeatherData.Temperature,
                Humidity = newHumidity
            };
            Console.WriteLine($"Humidity updated to {newHumidity}%");
        }

        if (!string.IsNullOrWhiteSpace(tempInput) || !string.IsNullOrWhiteSpace(humidityInput))
        {
            Console.WriteLine("\n--- Notifying all weather bots ---");
            weatherService.UpdateWeatherData(_currentWeatherData);
        }
        else
        {
            Console.WriteLine("No changes made.");
        }
    }
}