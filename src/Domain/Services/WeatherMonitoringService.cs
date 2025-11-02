using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.Domain.Services;

public class WeatherMonitoringService : IWeatherMonitoringService
{
    private readonly IParserSelector _parserSelector;
    private readonly List<IWeatherBot> _bots;
    private readonly ISubject _weatherDataSubject;

    public WeatherMonitoringService(IParserSelector parserSelector, IEnumerable<IWeatherBot> bots, ISubject weatherDataSubject)
    {
        _parserSelector = parserSelector;
        _bots = bots.ToList();
        _weatherDataSubject = weatherDataSubject;

        foreach (var bot in _bots)
        {
            _weatherDataSubject.Attach(bot);
        }
    }

    public WeatherData? ProcessWeatherInput(string input)
    {
        var parser = _parserSelector.SelectParser(input);
        if (parser == null)
        {
            Console.WriteLine("Unsupported data format");
            return null;
        }

        if (!parser.TryParse(input, out var weatherData) || weatherData == null)
        {
            Console.WriteLine("Failed to parse weather data");
            return null;
        }

        _weatherDataSubject.UpdateWeatherData(weatherData);

        return weatherData;
    }

    public void UpdateWeatherData(WeatherData weatherData)
    {
        _weatherDataSubject.UpdateWeatherData(weatherData);
    }
}