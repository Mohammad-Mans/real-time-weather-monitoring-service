using RTWMS.Domain.Bots;
using RTWMS.Domain.Enums;

namespace RTWMS.Domain.Interfaces;

public interface IWeatherBotFactory
{
    WeatherBot? GetBot(BotType botType, double threshold, string message);
}