using AutoFixture;
using RTWMS.Domain.Bots;
using RTWMS.Domain.Enums;
using RTWMS.Domain.Factories;
using RTWMS.Domain.Models;

namespace RTWMS.Tests.Domain.Factories;

public class WeatherBotFactoryTests
{
    private readonly WeatherBotFactory _factory;

    public WeatherBotFactoryTests()
    {
        _factory = new WeatherBotFactory();
    }

    [Fact]
    public void GetBot_ShouldCreateCorrectBotType_WhenValidBotType()
    {
        var fixture = new Fixture();
        var config = fixture.Create<BotConfig>();

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
        var fixture = new Fixture();
        var config = fixture.Create<BotConfig>();

        var result = _factory.GetBot(config, (BotType)999);

        result.Should().BeNull();
    }
}