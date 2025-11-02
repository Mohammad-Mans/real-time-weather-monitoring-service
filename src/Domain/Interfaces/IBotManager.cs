namespace RTWMS.Domain.Interfaces;

public interface IBotManager
{
    List<IWeatherBot> GetConfiguredBots();
}