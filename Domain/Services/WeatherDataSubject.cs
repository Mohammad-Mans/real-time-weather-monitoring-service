using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.Domain.Services;

public class WeatherDataSubject : ISubject
{
    private readonly List<IObserver> _observers = new();
    private WeatherData? _weatherData;

    public void Attach(IObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void UpdateWeatherData(WeatherData weatherData)
    {
        _weatherData = weatherData;
        Notify();
    }

    public void Notify()
    {
        if (_weatherData != null)
        {
            foreach (var observer in _observers)
            {
                observer.Update(_weatherData);
            }
        }
    }
}
