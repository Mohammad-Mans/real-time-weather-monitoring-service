using RTWMS.Domain.Bots;

namespace RTWMS.Domain.Interfaces;

public interface IBotConfigurationService
{
    List<WeatherBot> GetConfiguredBots();
}