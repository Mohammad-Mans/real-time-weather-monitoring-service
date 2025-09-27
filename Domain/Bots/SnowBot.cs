namespace RTWMS.Domain.Bots;

public class SnowBot(double temperatureThreshold, string message) : WeatherBot(message)
{
    public override void ProcessWeatherData(double temperature, double humidity)
    {
        if (temperature < temperatureThreshold)
            PrintWeatherCast();
    }
}