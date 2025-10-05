using RTWMS.Domain.Bots;
using RTWMS.Domain.Enums;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.Domain.Factories;

public sealed class WeatherBotFactory(IEnumerable<IWeatherBotDecorator> decorators) : IWeatherBotFactory
{
    private readonly IReadOnlyList<IWeatherBotDecorator> _decorators = decorators.ToList();

    public IWeatherBot? GetBot(BotConfig config, BotType botType)
    {
        IWeatherBot? bot = botType switch
        {
            BotType.RainBot => new RainBot(config.HumidityThreshold, config.Message),
            BotType.SunBot => new SunBot(config.TemperatureThreshold, config.Message),
            BotType.SnowBot => new SnowBot(config.TemperatureThreshold, config.Message),
            _ => null
        };
        if (bot is null) return null;

        foreach (var decorator in _decorators)
            bot = decorator.Apply(bot);

        return bot;
    }
}