using RTWMS.Domain.Interfaces;

namespace RTWMS.API;

public class WeatherMonitoringMenu(IWeatherMonitoringService weatherService)
{
    public void Run()
    {
        Console.WriteLine("Weather Monitoring System Started!");
        Console.WriteLine("Enter weather data (JSON or XML format):");
        Console.WriteLine("Type '0' to quit");
        Console.WriteLine();

        while (true)
        {
            Console.Write("Enter weather data: ");
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input) || input == "0")
            {
                Console.WriteLine("Goodbye!");
                break;
            }

            try
            {
                weatherService.ProcessWeatherInput(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine();
        }
    }
}