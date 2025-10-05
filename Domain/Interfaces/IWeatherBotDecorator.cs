namespace RTWMS.Domain.Interfaces;

public interface IWeatherBotDecorator
{
    IWeatherBot Apply(IWeatherBot weatherBot);
}