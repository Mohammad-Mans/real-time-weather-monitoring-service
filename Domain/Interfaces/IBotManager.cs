using RTWMS.Domain.Bots;

namespace RTWMS.Domain.Interfaces;

public interface IBotManager
{
    List<WeatherBot> GetConfiguredBots();
}
