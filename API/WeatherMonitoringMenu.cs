using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.API;

public class WeatherMonitoringMenu(IWeatherMonitoringService weatherService)
{
    private readonly WeatherDataEditMenu _editMenu = new(weatherService);
    private WeatherData? _lastProcessedData;

    public void Run()
    {
        Console.WriteLine("Weather Monitoring System Started!");

        while (true)
        {
            DisplayMenu();
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    EnterWeatherData();
                    break;
                case "2":
                    if (_lastProcessedData != null)
                    {
                        _editMenu.ShowEditMenu();
                    }
                    else
                    {
                        Console.WriteLine("No weather data to edit. Please enter data first.");
                    }

                    break;
                case "3":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("\n------------------------");
        Console.WriteLine("1. Enter Weather Data");
        Console.WriteLine("2. Edit Weather Data");
        Console.WriteLine("3. Quit");
        Console.WriteLine("------------------------");
        Console.Write("Enter your choice (1-3): ");
    }

    private void EnterWeatherData()
    {
        Console.WriteLine("\nEnter weather data (JSON or XML format):");
        Console.Write("Weather data: ");
        var input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("No data entered.");
            return;
        }

        try
        {
            var weatherData = weatherService.ProcessWeatherInput(input);

            if (weatherData != null)
            {
                _lastProcessedData = weatherData;
                _editMenu.SetCurrentWeatherData(weatherData);
                Console.WriteLine("Weather data processed successfully!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}