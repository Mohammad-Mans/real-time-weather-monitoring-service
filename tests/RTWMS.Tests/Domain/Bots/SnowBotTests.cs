using AutoFixture;
using RTWMS.Domain.Bots;
using RTWMS.Domain.Models;

namespace RTWMS.Tests.Domain.Bots;

public class SnowBotTests
{
    private const double DefaultTemperatureThreshold = 0.0;

    [Fact]
    public void Constructor_ShouldSetCorrectName()
    {
        var fixture = new Fixture();
        var snowBot = new SnowBot(DefaultTemperatureThreshold, fixture.Create<string>());
        snowBot.Name.Should().Be("SnowBot");
    }

    [Theory]
    [InlineData(-1.0, true)]
    [InlineData(0.0, false)]
    [InlineData(1.0, false)]
    [InlineData(-20.0, true)]
    [InlineData(30.0, false)]
    public void TryProcessData_ShouldReturnCorrectResult_WhenTemperatureIsGiven(
        double temperature,
        bool expectedResult)
    {
        var fixture = new Fixture();
        var snowBot = new SnowBot(DefaultTemperatureThreshold, fixture.Create<string>());
        var weatherData = fixture.Build<WeatherData>()
            .With(data => data.Temperature, temperature)
            .Create();

        var result = snowBot.TryProcessData(weatherData);

        result.Should().Be(expectedResult);
    }
}