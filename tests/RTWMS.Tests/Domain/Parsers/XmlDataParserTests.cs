using RTWMS.Domain.Parsers;

namespace RTWMS.Tests.Domain.Parsers;

public class XmlDataParserTests
{
    private readonly XmlDataParser _parser = new();

    [Fact]
    public void TryParse_ShouldReturnTrue_WhenValidXml()
    {
        const string validXml =
            "<WeatherData><Location>Test City</Location><Temperature>25.5</Temperature><Humidity>60</Humidity></WeatherData>";

        var result = _parser.TryParse(validXml, out var data);

        result.Should().BeTrue();
        data.Should().NotBeNull();
        data.Location.Should().Be("Test City");
        data.Temperature.Should().Be(25.5);
        data.Humidity.Should().Be(60.0);
    }

    [Fact]
    public void TryParse_ShouldReturnFalse_WhenInvalidXml()
    {
        const string invalidXml = "<invalid xml structure";

        var result = _parser.TryParse(invalidXml, out var data);

        result.Should().BeFalse();
        data.Should().BeNull();
    }
}