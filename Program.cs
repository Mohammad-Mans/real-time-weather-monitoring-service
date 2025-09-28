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

        var botConfigService = new BotConfigurationService(botFactory);
        var weatherService = new WeatherMonitoringService(parserFactory, botConfigService);

        var mainMenu = new WeatherMonitoringMenu(weatherService);
        mainMenu.Run();
    }
}