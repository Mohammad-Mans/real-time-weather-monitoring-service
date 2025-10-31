using AutoFixture;
using RTWMS.Domain.Bots;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.Tests.Domain.Bots;

public class WeatherBotTests
{
    private const double RainHumidityThreshold = 70.0;
    private const double SunTemperatureThreshold = 30.0;
    private const double SnowTemperatureThreshold = 0.0;

    [Fact]
    public void Name_ShouldReturnCorrectTypeName()
    {
        var fixture = new Fixture();
        var rainBot = new RainBot(RainHumidityThreshold, fixture.Create<string>());
        var sunBot = new SunBot(SunTemperatureThreshold, fixture.Create<string>());
        var snowBot = new SnowBot(SnowTemperatureThreshold, fixture.Create<string>());

        rainBot.Name.Should().Be("RainBot");
        sunBot.Name.Should().Be("SunBot");
        snowBot.Name.Should().Be("SnowBot");
    }

    [Fact]
    public void Update_ShouldCallTryProcessData()
    {
        var fixture = new Fixture();
        var mockRainBot = new Mock<RainBot>(RainHumidityThreshold, fixture.Create<string>()) { CallBase = true };
        var weatherData = fixture.Create<WeatherData>();

        mockRainBot.Object.Update(weatherData);

        mockRainBot.Verify(bot => bot.TryProcessData(weatherData), Times.Once);
    }

    [Fact]
    public void WeatherBot_ShouldImplementIWeatherBot()
    {
        var fixture = new Fixture();
        var rainBot = new RainBot(RainHumidityThreshold, fixture.Create<string>());
        rainBot.Should().BeAssignableTo<IWeatherBot>();
    }
}