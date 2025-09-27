namespace RTWMS.Domain.Bots;

public class SunBot(double temperatureThreshold, string message) : WeatherBot(message)
{
    public override void ProcessWeatherData(double temperature, double humidity)
    {
        if (temperature > temperatureThreshold)
            PrintWeatherCast();
    }
}