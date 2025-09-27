using RTWMS.Domain.Bots;
using RTWMS.Domain.Enums;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;
using RTWMS.Utils;

namespace RTWMS.Domain.Services;

public class BotConfigurationService(IWeatherBotFactory botFactory) : IBotConfigurationService
{
    public List<WeatherBot> GetConfiguredBots()
    {
        var config = ConfigurationReader.ReadBotConfiguration();
        var bots = new List<WeatherBot>();

        if (config.RainBot.Enabled)
        {
            var rainBot = botFactory.GetBot(BotType.RainBot, config.RainBot.HumidityThreshold, config.RainBot.Message);
            if (rainBot != null) bots.Add(rainBot);
        }

        if (config.SunBot.Enabled)
        {
            var sunBot = botFactory.GetBot(BotType.SunBot, config.SunBot.TemperatureThreshold, config.SunBot.Message);
            if (sunBot != null) bots.Add(sunBot);
        }

        if (config.SnowBot.Enabled)
        {
            var snowBot = botFactory.GetBot(BotType.SnowBot, config.SnowBot.TemperatureThreshold, config.SnowBot.Message);
            if (snowBot != null) bots.Add(snowBot);
        }

        return bots;
    }
}
