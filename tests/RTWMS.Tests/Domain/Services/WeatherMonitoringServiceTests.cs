using AutoFixture;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;
using RTWMS.Domain.Services;

namespace RTWMS.Tests.Domain.Services;

public class WeatherMonitoringServiceTests
{
    private readonly Mock<IParserSelector> _mockParserSelector;
    private readonly Mock<ISubject> _mockWeatherDataSubject;
    private readonly Mock<IDataParser> _mockDataParser;
    private readonly Mock<IWeatherBot> _mockWeatherBot;
    private readonly WeatherMonitoringService _service;

    public WeatherMonitoringServiceTests()
    {
        _mockParserSelector = new Mock<IParserSelector>();
        _mockWeatherDataSubject = new Mock<ISubject>();
        _mockDataParser = new Mock<IDataParser>();
        _mockWeatherBot = new Mock<IWeatherBot>();

        var bots = new List<IWeatherBot> { _mockWeatherBot.Object };

        _service = new WeatherMonitoringService(
            _mockParserSelector.Object,
            bots,
            _mockWeatherDataSubject.Object);
    }

    [Fact]
    public void ProcessWeatherInput_ShouldReturnWeatherData_WhenValidInputAndSuccessfulParsing()
    {
        var fixture = new Fixture();
        var input = fixture.Create<string>();
        var expectedWeatherData = fixture.Create<WeatherData>();

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
        var fixture = new Fixture();
        var input = fixture.Create<string>();
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
        var fixture = new Fixture();
        var input = fixture.Create<string>();
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
        var fixture = new Fixture();
        var weatherData = fixture.Create<WeatherData>();

        _service.UpdateWeatherData(weatherData);

        _mockWeatherDataSubject.Verify(subject => subject.UpdateWeatherData(weatherData), Times.Once);
    }
}