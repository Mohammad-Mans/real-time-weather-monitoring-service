using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;
using System.Diagnostics;

namespace RTWMS.Domain.Decorators;

public sealed class LoggingWeatherBotDecorator(IWeatherBot weatherBot) : IWeatherBot
{
    public string Name => weatherBot.Name;

    public bool TryProcessData(WeatherData data)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            var result = weatherBot.TryProcessData(data);
            sw.Stop();

            if (result)
            {
                Console.WriteLine(
                    $"[{DateTime.Now:HH:mm:ss.fff}] {weatherBot.Name}: processed in {sw.ElapsedMilliseconds}ms");
            }

            return result;
        }
        catch (Exception ex)
        {
            sw.Stop();
            Console.WriteLine(
                $"[{DateTime.Now:HH:mm:ss.fff}] {weatherBot.Name}: failed after {sw.ElapsedMilliseconds}ms — {ex.Message}");
            throw;
        }
    }
}