using RTWMS.Domain.Models;

namespace RTWMS.Domain.Interfaces;

public interface IWeatherBot : IObserver
{
    string Name { get; }
    bool TryProcessData(WeatherData data);
}