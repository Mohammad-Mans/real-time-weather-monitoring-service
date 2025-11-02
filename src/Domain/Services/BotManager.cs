using RTWMS.Domain.Enums;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;
using RTWMS.Utils;

namespace RTWMS.Domain.Services;

public class BotManager : IBotManager
{
    private static BotManager? _instance;
    private static readonly object _lock = new();
    private readonly IWeatherBotFactory _botFactory;

    private BotManager(IWeatherBotFactory botFactory)
    {
        _botFactory = botFactory;
    }

    public static BotManager GetInstance(IWeatherBotFactory botFactory)
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new BotManager(botFactory);
                }
            }
        }

        return _instance;
    }

    public List<IWeatherBot> GetConfiguredBots()
    {
        var configuredBots = new List<IWeatherBot>();
        var allBotsConfigs = ConfigurationReader.ReadBotConfiguration();
        var configsProperties = allBotsConfigs?.GetType().GetProperties();

        if (configsProperties is null) return configuredBots;

        foreach (var botProperty in configsProperties)
        {
            var propertyName = botProperty.Name;
            var propertyValue = botProperty.GetValue(allBotsConfigs) as BotConfig;

            if (propertyValue is null || !propertyValue.Enabled) continue;

            if (Enum.TryParse<BotType>(propertyName, out var botType))
            {
                var bot = _botFactory.GetBot(propertyValue, botType);
                if (bot is not null) configuredBots.Add(bot);
            }
        }

        return configuredBots;
    }
}