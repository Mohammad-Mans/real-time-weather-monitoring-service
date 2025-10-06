using RTWMS.Domain.Bots;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.Tests.Domain.Bots;

public class WeatherBotTests
{
    private const string TestMessage = "Test message";

    [Fact]
    public void Name_ShouldReturnCorrectTypeName()
    {
        var rainBot = new RainBot(70.0, TestMessage);
        var sunBot = new SunBot(30.0, TestMessage);
        var snowBot = new SnowBot(0.0, TestMessage);

        rainBot.Name.Should().Be("RainBot");
        sunBot.Name.Should().Be("SunBot");
        snowBot.Name.Should().Be("SnowBot");
    }

    [Fact]
    public void Update_ShouldCallTryProcessData()
    {
        var mockRainBot = new Mock<RainBot>(70.0, TestMessage) { CallBase = true };
        var weatherData = new WeatherData { Humidity = 75.0 };

        mockRainBot.Object.Update(weatherData);

        mockRainBot.Verify(x => x.TryProcessData(weatherData), Times.Once);
    }

    [Fact]
    public void WeatherBot_ShouldImplementIWeatherBot()
    {
        var rainBot = new RainBot(70.0, TestMessage);
        rainBot.Should().BeAssignableTo<IWeatherBot>();
    }
}