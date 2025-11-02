using RTWMS.Domain.Models;

namespace RTWMS.Domain.Interfaces;

public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
    void UpdateWeatherData(WeatherData weatherData);
}
