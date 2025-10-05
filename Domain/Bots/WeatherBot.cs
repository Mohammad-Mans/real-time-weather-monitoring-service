using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.Domain.Bots;

public abstract class WeatherBot(string message) : IWeatherBot
{
    public string Name => GetType().Name;

    protected void PrintWeatherCast()
    {
        Console.WriteLine($"{Name} activated!");
        Console.WriteLine($"{Name}: \"{message}\"");
    }

    public abstract bool TryProcessData(WeatherData data);
}