using RTWMS.Domain.Bots;
using RTWMS.Domain.Enums;
using RTWMS.Domain.Interfaces;

namespace RTWMS.Domain.Factories;

public class WeatherBotFactory : IWeatherBotFactory
{
    public WeatherBot? GetBot(BotType botType, double threshold, string message)
    {
        return botType switch
        {
            BotType.RainBot => new RainBot(threshold, message),
            BotType.SunBot => new SunBot(threshold, message),
            BotType.SnowBot => new SnowBot(threshold, message),
            _ => null
        };
    }
}