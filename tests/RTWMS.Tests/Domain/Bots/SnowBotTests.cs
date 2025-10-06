using RTWMS.Domain.Bots;
using RTWMS.Domain.Models;

namespace RTWMS.Tests.Domain.Bots;

public class SnowBotTests
{
    private const double DefaultTemperatureThreshold = 0.0;
    private const string DefaultMessage = "Test message";

    [Fact]
    public void Constructor_ShouldSetCorrectName()
    {
        var snowBot = new SnowBot(0.0, "Test message");
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
        var snowBot = new SnowBot(DefaultTemperatureThreshold, DefaultMessage);
        var weatherData = new WeatherData
        {
            Location = "Test City",
            Temperature = temperature,
            Humidity = 50.0
        };

        var result = snowBot.TryProcessData(weatherData);

        result.Should().Be(expectedResult);
    }
}