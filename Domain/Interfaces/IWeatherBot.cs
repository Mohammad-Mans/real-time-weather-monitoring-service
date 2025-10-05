namespace RTWMS.Domain.Interfaces;

public interface IWeatherBot
{
    string Name { get; }
    void ProcessWeatherData(double temperature, double humidity);
}