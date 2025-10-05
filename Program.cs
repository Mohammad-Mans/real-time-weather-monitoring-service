using RTWMS.API;
using RTWMS.Domain.Factories;
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

        var weatherService = new WeatherMonitoringService(parserSelector, botManager);

        var mainMenu = new WeatherMonitoringMenu(weatherService);
        mainMenu.Run();
    }
}