using RTWMS.API;
using RTWMS.Domain.Factories;
using RTWMS.Domain.Services;

namespace RTWMS;

class Program
{
    static void Main(string[] args)
    {
        var botFactory = new WeatherBotFactory();

        var botConfigService = new BotConfigurationService(botFactory);
    }
}