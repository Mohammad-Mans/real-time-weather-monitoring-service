using RTWMS.Domain.Models;

namespace RTWMS.Domain.Interfaces;

public interface IDataParser
{
    bool TryParse(string input, out WeatherData? data);
}