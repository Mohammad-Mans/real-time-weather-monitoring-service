using RTWMS.API;
using RTWMS.Domain.Decorators;
using RTWMS.Domain.Factories;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Parsers;
using RTWMS.Domain.Services;

namespace RTWMS;

class Program
{
    static void Main(string[] args)
    {
        var decorators = new IWeatherBotDecorator[]
        {
            new LoggingDecorator(),
        };

        var botFactory = new WeatherBotFactory(decorators);
        var botManager = BotManager.GetInstance(botFactory);
        var parserSelector = new ParserSelector();
        var weatherDataSubject = new WeatherDataSubject();

        var weatherService = new WeatherMonitoringService(parserSelector, botManager, weatherDataSubject);

        var mainMenu = new WeatherMonitoringMenu(weatherService);
        mainMenu.Run();
    }
}