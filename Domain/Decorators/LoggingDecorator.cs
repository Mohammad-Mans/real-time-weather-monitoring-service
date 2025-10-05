using RTWMS.Domain.Interfaces;

namespace RTWMS.Domain.Decorators;

public sealed class LoggingDecorator : IWeatherBotDecorator
{
    public IWeatherBot Apply(IWeatherBot weatherBot) => new LoggingWeatherBotDecorator(weatherBot);
}