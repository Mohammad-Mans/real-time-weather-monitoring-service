using RTWMS.Domain.Models;

namespace RTWMS.Domain.Interfaces;

public interface IDataParser
{
    WeatherData? Parse(string input);
    static abstract bool IsValidFormat(string input);
}