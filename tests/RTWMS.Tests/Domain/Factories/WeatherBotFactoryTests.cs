using RTWMS.Domain.Bots;
using RTWMS.Domain.Enums;
using RTWMS.Domain.Factories;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.Tests.Domain.Factories;

public class WeatherBotFactoryTests
{
    private readonly Mock<IWeatherBotDecorator> _mockDecorator;
    private readonly WeatherBotFactory _factory;

    public WeatherBotFactoryTests()
    {
        _mockDecorator = new Mock<IWeatherBotDecorator>();
        _factory = new WeatherBotFactory([_mockDecorator.Object]);
    }

    [Fact]
    public void GetBot_ShouldCreateCorrectBotType_WhenValidBotType()
    {
        var config = new BotConfig
        {
            HumidityThreshold = 80.0,
            TemperatureThreshold = 30.0,
            Message = "Test message"
        };

        _mockDecorator.Setup(decorator => decorator.Apply(It.IsAny<IWeatherBot>()))
            .Returns((IWeatherBot bot) => bot);

        var rainBot = _factory.GetBot(config, BotType.RainBot);
        var sunBot = _factory.GetBot(config, BotType.SunBot);
        var snowBot = _factory.GetBot(config, BotType.SnowBot);

        rainBot.Should().BeOfType<RainBot>();
        sunBot.Should().BeOfType<SunBot>();
        snowBot.Should().BeOfType<SnowBot>();
    }

    [Fact]
    public void GetBot_ShouldReturnNull_WhenBotTypeIsUnknown()
    {
        var config = new BotConfig
        {
            TemperatureThreshold = 25.0,
            Message = "Test message"
        };

        var result = _factory.GetBot(config, (BotType)999);

        result.Should().BeNull();
        _mockDecorator.Verify(decorator => decorator.Apply(It.IsAny<IWeatherBot>()), Times.Never);
    }

    [Fact]
    public void GetBot_ShouldApplyDecorators_WhenDecoratorsProvided()
    {
        var config = new BotConfig
        {
            HumidityThreshold = 80.0,
            Message = "Rain alert message"
        };

        _mockDecorator.Setup(decorator => decorator.Apply(It.IsAny<IWeatherBot>()))
            .Returns((IWeatherBot bot) => bot);

        var result = _factory.GetBot(config, BotType.RainBot);

        result.Should().NotBeNull();
        _mockDecorator.Verify(decorator => decorator.Apply(It.IsAny<IWeatherBot>()), Times.Once);
    }
}