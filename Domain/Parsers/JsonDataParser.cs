using System.Text.Json;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.Domain.Parsers;

public class JsonDataParser : IDataParser
{
    public WeatherData? Parse(string input)
    {
        try
        {
            return JsonSerializer.Deserialize<WeatherData>(input);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    public static bool IsValidFormat(string input)
    {
        try
        {
            JsonSerializer.Deserialize<WeatherData>(input);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}