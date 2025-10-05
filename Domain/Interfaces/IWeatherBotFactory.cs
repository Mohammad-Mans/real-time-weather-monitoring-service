using RTWMS.Domain.Enums;
using RTWMS.Domain.Models;

namespace RTWMS.Domain.Interfaces;

public interface IWeatherBotFactory
{
    IWeatherBot? GetBot(BotConfig config, BotType botType);
}