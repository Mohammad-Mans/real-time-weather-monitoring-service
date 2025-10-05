using RTWMS.Domain.Interfaces;

namespace RTWMS.Domain.Bots;

public abstract class WeatherBot(string message) : IWeatherBot
{
    public string Name => GetType().Name;

    protected void PrintWeatherCast()
    {
        Console.WriteLine($"{Name} activated!");
        Console.WriteLine($"{Name}: \"{message}\"");
    }

    public abstract void ProcessWeatherData(double temperature, double humidity);
}