using RTWMS.Domain.Interfaces;

namespace RTWMS.Domain.Services;

public class WeatherMonitoringService(IParserSelector parserSelector, IBotManager botManager)
    : IWeatherMonitoringService
{
    private readonly List<IWeatherBot> _bots = botManager.GetConfiguredBots();

    public void ProcessWeatherInput(string input)
    {
        var parser = parserSelector.SelectParser(input);
        if (parser == null)
        {
            Console.WriteLine("Unsupported data format");
            return;
        }

        if (!parser.TryParse(input, out var weatherData) || weatherData == null)
        {
            Console.WriteLine("Failed to parse weather data");
            return;
        }

        foreach (var bot in _bots)
        {
            bot.TryProcessData(weatherData);
        }
    }
}