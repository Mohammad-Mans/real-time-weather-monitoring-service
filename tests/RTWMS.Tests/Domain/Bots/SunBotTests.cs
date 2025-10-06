using RTWMS.Domain.Bots;
using RTWMS.Domain.Models;

namespace RTWMS.Tests.Domain.Bots;

public class SunBotTests
{
    private const double DefaultTemperatureThreshold = 30.0;
    private const string DefaultMessage = "Test message";

    [Fact]
    public void Constructor_ShouldSetCorrectName()
    {
        var sunBot = new SunBot(30.0, "Test message");
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
        var sunBot = new SunBot(DefaultTemperatureThreshold, DefaultMessage);
        var weatherData = new WeatherData
        {
            Location = "Test City",
            Temperature = temperature,
            Humidity = 50.0
        };

        var result = sunBot.TryProcessData(weatherData);

        result.Should().Be(expectedResult);
    }
}