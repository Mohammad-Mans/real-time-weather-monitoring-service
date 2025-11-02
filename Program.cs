using System.Linq;
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
        var botFactory = new WeatherBotFactory();
        var botManager = BotManager.GetInstance(botFactory);
        var parserSelector = new ParserSelector();
        var weatherDataSubject = new WeatherDataSubject();

        var bots = botManager.GetConfiguredBots();
        var decoratedBots = bots.Select(bot =>
            new LoggingWeatherBotDecorator(
                new NotificationWeatherBotDecorator(bot)
            )).ToList();

        var weatherService = new WeatherMonitoringService(parserSelector, decoratedBots, weatherDataSubject);

        var mainMenu = new WeatherMonitoringMenu(weatherService);
        mainMenu.Run();
    }
}