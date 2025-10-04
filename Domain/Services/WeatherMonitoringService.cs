using RTWMS.Domain.Bots;
using RTWMS.Domain.Interfaces;

namespace RTWMS.Domain.Services;

public class WeatherMonitoringService(IParserFactory parserFactory, IBotManager botManager)
    : IWeatherMonitoringService
{
    private readonly List<WeatherBot> _bots = botManager.GetConfiguredBots();

    public void ProcessWeatherInput(string input)
    {
        var parser = parserFactory.GetParser(input);
        if (parser == null)
        {
            Console.WriteLine("Unsupported data format");
            return;
        }

        var weatherData = parser.Parse(input);
        if (weatherData == null)
        {
            Console.WriteLine("Failed to parse weather data");
            return;
        }

        foreach (var bot in _bots)
        {
            bot.ProcessWeatherData(weatherData.Temperature, weatherData.Humidity);
        }
    }
}