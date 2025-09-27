namespace RTWMS.Domain.Bots;

public class RainBot(double humidityThreshold, string message) : WeatherBot(message)
{
    public override void ProcessWeatherData(double temperature, double humidity)
    {
        if (humidity > humidityThreshold)
            PrintWeatherCast();
    }
}