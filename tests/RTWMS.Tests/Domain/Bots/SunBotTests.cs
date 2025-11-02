using AutoFixture;
using RTWMS.Domain.Bots;
using RTWMS.Domain.Models;

namespace RTWMS.Tests.Domain.Bots;

public class SunBotTests
{
    private const double DefaultTemperatureThreshold = 30.0;

    [Fact]
    public void Constructor_ShouldSetCorrectName()
    {
        var fixture = new Fixture();
        var sunBot = new SunBot(DefaultTemperatureThreshold, fixture.Create<string>());
        sunBot.Name.Should().Be("SunBot");
    }

    [Theory]
    [InlineData(31.0, true)]
    [InlineData(30.0, false)]
    [InlineData(29.9, false)]
    [InlineData(50.0, true)]
    [InlineData(-10.0, false)]
    public void TryProcessData_ShouldReturnCorrectResult_WhenTemperatureIsGiven(
        double temperature,
        bool expectedResult)
    {
        var fixture = new Fixture();
        var sunBot = new SunBot(DefaultTemperatureThreshold, fixture.Create<string>());
        var weatherData = fixture.Build<WeatherData>()
            .With(data => data.Temperature, temperature)
            .Create();

        var result = sunBot.TryProcessData(weatherData);

        result.Should().Be(expectedResult);
    }
}