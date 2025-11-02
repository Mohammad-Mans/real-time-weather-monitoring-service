using RTWMS.Domain.Models;

namespace RTWMS.Domain.Interfaces;

public interface IObserver
{
    void Update(WeatherData weatherData);
}
