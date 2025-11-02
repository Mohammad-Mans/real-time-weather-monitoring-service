using System.Text.Json;
using AutoFixture;
using RTWMS.Domain.Bots;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;
using RTWMS.Domain.Parsers;
using RTWMS.Domain.Services;

namespace RTWMS.Tests.Integration;

public class WeatherMonitoringIntegrationTests
{
    private const double RainBotHumidityThreshold = 70.0;
    private const double HighHumidity = 75.0;

    [Fact]
    public void ProcessWeatherInput_ShouldParseAndNotifyBots_WhenValidJsonInput()
    {
        var fixture = new Fixture();
        var weatherData = fixture.Build<WeatherData>()
            .With(data => data.Humidity, HighHumidity)
            .Create();

        var jsonInput = JsonSerializer.Serialize(weatherData);

        var parserSelector = new ParserSelector();
        var weatherDataSubject = new WeatherDataSubject();

        var rainBot = new RainBot(RainBotHumidityThreshold, fixture.Create<string>());
        var bots = new List<IWeatherBot> { rainBot };

        var service = new WeatherMonitoringService(parserSelector, bots, weatherDataSubject);

        var result = service.ProcessWeatherInput(jsonInput);
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(weatherData);
        rainBot.TryProcessData(weatherData).Should().BeTrue();
    }
}

