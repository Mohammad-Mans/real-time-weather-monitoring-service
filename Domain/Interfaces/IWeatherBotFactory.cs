using RTWMS.Domain.Bots;
using RTWMS.Domain.Enums;
using RTWMS.Domain.Models;

namespace RTWMS.Domain.Interfaces;

public interface IWeatherBotFactory
{
    WeatherBot? GetBot(BotConfig config, BotType botType);
}