using RTWMS.Domain.Bots;
using RTWMS.Domain.Enums;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.Domain.Factories;

public class WeatherBotFactory : IWeatherBotFactory
{
    public WeatherBot? GetBot(BotConfig config, BotType botType)
    {
        return botType switch
        {
            BotType.RainBot => new RainBot(config.HumidityThreshold, config.Message),
            BotType.SunBot => new SunBot(config.TemperatureThreshold, config.Message),
            BotType.SnowBot => new SnowBot(config.TemperatureThreshold, config.Message),
            _ => null
        };
    }
}