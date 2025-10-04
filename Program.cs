using RTWMS.API;
using RTWMS.Domain.Factories;
using RTWMS.Domain.Services;

namespace RTWMS;

class Program
{
    static void Main(string[] args)
    {
        var botFactory = new WeatherBotFactory();
        var parserFactory = new ParserFactory();

        var botManager = BotManager.GetInstance(botFactory);
        var weatherService = new WeatherMonitoringService(parserFactory, botManager);

        var mainMenu = new WeatherMonitoringMenu(weatherService);
        mainMenu.Run();
    }
}