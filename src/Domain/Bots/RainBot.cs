using RTWMS.Domain.Models;

namespace RTWMS.Domain.Bots;

public class RainBot(double humidityThreshold, string message) : WeatherBot(message)
{
    public override bool TryProcessData(WeatherData data)
    {
        if (data.Humidity > humidityThreshold)
        {
            PrintWeatherCast();
            return true;
        }

        return false;
    }
}