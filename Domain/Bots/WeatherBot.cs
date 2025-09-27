namespace RTWMS.Domain.Bots;

public abstract class WeatherBot(string message)
{
    private string BotName => GetType().Name;

    protected void PrintWeatherCast()
    {
        Console.WriteLine($"{BotName} activated!");
        Console.WriteLine($"{BotName}: \"{message}\"");
    }

    public abstract void ProcessWeatherData(double temperature, double humidity);
}