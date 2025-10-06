using RTWMS.Domain.Bots;
using RTWMS.Domain.Models;

namespace RTWMS.Tests.Domain.Bots;

public class RainBotTests
{
    private const double DefaultHumidityThreshold = 70.0;
    private const string DefaultMessage = "Test message";

    [Fact]
    public void Constructor_ShouldSetCorrectName()
    {
        var rainBot = new RainBot(70.0, "Test message");
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
        var rainBot = new RainBot(DefaultHumidityThreshold, DefaultMessage);
        var weatherData = new WeatherData
        {
            Location = "Test City",
            Temperature = 25.0,
            Humidity = humidity
        };

        var result = rainBot.TryProcessData(weatherData);

        result.Should().Be(expectedResult);
    }
}