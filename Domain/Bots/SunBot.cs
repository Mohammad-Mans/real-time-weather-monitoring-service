using RTWMS.Domain.Models;

namespace RTWMS.Domain.Bots;

public class SunBot(double temperatureThreshold, string message) : WeatherBot(message)
{
    public override bool TryProcessData(WeatherData data)
    {
        if (data.Temperature > temperatureThreshold)
        {
            PrintWeatherCast();
            return true;
        }

        return false;
    }
}