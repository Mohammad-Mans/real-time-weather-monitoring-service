using System.Text.Json;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.Domain.Parsers;

public sealed class JsonDataParser : IDataParser
{
    public bool TryParse(string input, out WeatherData? data)
    {
        try
        {
            data = JsonSerializer.Deserialize<WeatherData>(input);
            return data is not null;
        }
        catch (JsonException)
        {
            data = null;
            return false;
        }
    }
}