using AutoFixture;
using RTWMS.Domain.Bots;
using RTWMS.Domain.Models;

namespace RTWMS.Tests.Domain.Bots;

public class RainBotTests
{
    private const double DefaultHumidityThreshold = 70.0;

    [Fact]
    public void Constructor_ShouldSetCorrectName()
    {
        var fixture = new Fixture();
        var rainBot = new RainBot(DefaultHumidityThreshold, fixture.Create<string>());
        rainBot.Name.Should().Be("RainBot");
    }

    [Theory]
    [InlineData(71.0, true)]
    [InlineData(70.0, false)]
    [InlineData(69.9, false)]
    [InlineData(100.0, true)]
    [InlineData(0.0, false)]
    public void TryProcessData_ShouldReturnCorrectResult_WhenHumidityIsGiven(
        double humidity,
        bool expectedResult)
    {
        var fixture = new Fixture();
        var rainBot = new RainBot(DefaultHumidityThreshold, fixture.Create<string>());
        var weatherData = fixture.Build<WeatherData>()
            .With(data => data.Humidity, humidity)
            .Create();

        var result = rainBot.TryProcessData(weatherData);

        result.Should().Be(expectedResult);
    }
}