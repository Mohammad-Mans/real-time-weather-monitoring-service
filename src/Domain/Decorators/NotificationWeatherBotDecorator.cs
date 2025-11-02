using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.Domain.Decorators;

public sealed class NotificationWeatherBotDecorator(IWeatherBot weatherBot) : IWeatherBot
{
    public string Name => weatherBot.Name;

    public bool TryProcessData(WeatherData data)
    {
        Console.WriteLine($"[Notification] {weatherBot.Name} is processing weather data from {data.Location}");

        var result = weatherBot.TryProcessData(data);

        if (result)
        {
            Console.WriteLine($"[Notification] {weatherBot.Name} activated for {data.Location}!");
        }
        else
        {
            Console.WriteLine($"[Notification] {weatherBot.Name} did not activate (threshold not met)");
        }

        return result;
    }

    public void Update(WeatherData weatherData)
    {
        TryProcessData(weatherData);
    }
}


