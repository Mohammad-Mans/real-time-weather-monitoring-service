using AutoFixture;
using RTWMS.Domain.Parsers;
using RTWMS.Domain.Models;

namespace RTWMS.Tests.Domain.Parsers;

public class XmlDataParserTests
{
    private readonly XmlDataParser _parser = new();

    [Fact]
    public void TryParse_ShouldReturnTrue_WhenValidXml()
    {
        var fixture = new Fixture();
        var expectedData = fixture.Create<WeatherData>();
        var validXml =
            $"<WeatherData><Location>{expectedData.Location}</Location><Temperature>{expectedData.Temperature}</Temperature><Humidity>{expectedData.Humidity}</Humidity></WeatherData>";

        var result = _parser.TryParse(validXml, out var data);

        result.Should().BeTrue();
        data.Should().NotBeNull();
        data.Should().BeEquivalentTo(expectedData);
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