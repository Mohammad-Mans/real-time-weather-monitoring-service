using RTWMS.Domain.Models;

namespace RTWMS.Domain.Interfaces;

public interface IWeatherMonitoringService
{
    WeatherData? ProcessWeatherInput(string input);
    void UpdateWeatherData(WeatherData weatherData);
}