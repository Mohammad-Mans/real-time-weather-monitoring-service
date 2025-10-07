using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;
using RTWMS.Domain.Services;

namespace RTWMS.Tests.Domain.Services;

public class WeatherMonitoringServiceTests
{
    private readonly Mock<IParserSelector> _mockParserSelector;
    private readonly Mock<IBotManager> _mockBotManager;
    private readonly Mock<ISubject> _mockWeatherDataSubject;
    private readonly Mock<IDataParser> _mockDataParser;
    private readonly Mock<IWeatherBot> _mockWeatherBot;
    private readonly WeatherMonitoringService _service;

    public WeatherMonitoringServiceTests()
    {
        _mockParserSelector = new Mock<IParserSelector>();
        _mockBotManager = new Mock<IBotManager>();
        _mockWeatherDataSubject = new Mock<ISubject>();
        _mockDataParser = new Mock<IDataParser>();
        _mockWeatherBot = new Mock<IWeatherBot>();

        var bots = new List<IWeatherBot> { _mockWeatherBot.Object };
        _mockBotManager.Setup(manager => manager.GetConfiguredBots()).Returns(bots);

        _service = new WeatherMonitoringService(
            _mockParserSelector.Object,
            _mockBotManager.Object,
            _mockWeatherDataSubject.Object);
    }

    [Fact]
    public void ProcessWeatherInput_ShouldReturnWeatherData_WhenValidInputAndSuccessfulParsing()
    {
        const string input = "valid weather data";
        var expectedWeatherData = new WeatherData
        {
            Location = "Test City",
            Temperature = 25.5,
            Humidity = 60.0
        };

        _mockParserSelector.Setup(selector => selector.SelectParser(input)).Returns(_mockDataParser.Object);
        _mockDataParser.Setup(parser => parser.TryParse(input, out expectedWeatherData)).Returns(true);

        var result = _service.ProcessWeatherInput(input);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedWeatherData);
        _mockParserSelector.Verify(selector => selector.SelectParser(input), Times.Once);
        _mockDataParser.Verify(parser => parser.TryParse(input, out expectedWeatherData), Times.Once);
        _mockWeatherDataSubject.Verify(subject => subject.UpdateWeatherData(expectedWeatherData), Times.Once);
    }

    [Fact]
    public void ProcessWeatherInput_ShouldReturnNull_WhenNoParserFound()
    {
        const string input = "unsupported format";
        _mockParserSelector.Setup(selector => selector.SelectParser(input)).Returns((IDataParser?)null);

        var result = _service.ProcessWeatherInput(input);

        result.Should().BeNull();
        _mockParserSelector.Verify(selector => selector.SelectParser(input), Times.Once);
        _mockDataParser.Verify(parser => parser.TryParse(It.IsAny<string>(), out It.Ref<WeatherData?>.IsAny),
            Times.Never);
        _mockWeatherDataSubject.Verify(subject => subject.UpdateWeatherData(It.IsAny<WeatherData>()), Times.Never);
    }

    [Fact]
    public void ProcessWeatherInput_ShouldReturnNull_WhenParsingFails()
    {
        const string input = "invalid weather data";
        WeatherData? nullWeatherData = null;

        _mockParserSelector.Setup(selector => selector.SelectParser(input)).Returns(_mockDataParser.Object);
        _mockDataParser.Setup(parser => parser.TryParse(input, out nullWeatherData)).Returns(false);

        var result = _service.ProcessWeatherInput(input);

        result.Should().BeNull();
        _mockParserSelector.Verify(selector => selector.SelectParser(input), Times.Once);
        _mockDataParser.Verify(parser => parser.TryParse(input, out nullWeatherData), Times.Once);
        _mockWeatherDataSubject.Verify(subject => subject.UpdateWeatherData(It.IsAny<WeatherData>()), Times.Never);
    }

    [Fact]
    public void ProcessWeatherInput_ShouldHandleEmptyInput()
    {
        const string input = "";
        _mockParserSelector.Setup(selector => selector.SelectParser(input)).Returns((IDataParser?)null);

        var result = _service.ProcessWeatherInput(input);

        result.Should().BeNull();
        _mockParserSelector.Verify(selector => selector.SelectParser(input), Times.Once);
    }

    [Fact]
    public void UpdateWeatherData_ShouldDelegateToSubject()
    {
        var weatherData = new WeatherData
        {
            Location = "Test City",
            Temperature = 25.5,
            Humidity = 60.0
        };

        _service.UpdateWeatherData(weatherData);

        _mockWeatherDataSubject.Verify(subject => subject.UpdateWeatherData(weatherData), Times.Once);
    }
}