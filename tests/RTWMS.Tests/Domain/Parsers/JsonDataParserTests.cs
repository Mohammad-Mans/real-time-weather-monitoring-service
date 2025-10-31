using System.Text.Json;
using AutoFixture;
using RTWMS.Domain.Parsers;
using RTWMS.Domain.Models;

namespace RTWMS.Tests.Domain.Parsers;

public class JsonDataParserTests
{
    private readonly JsonDataParser _parser = new();

    [Fact]
    public void TryParse_ShouldReturnTrue_WhenValidJson()
    {
        var fixture = new Fixture();
        var expectedData = fixture.Create<WeatherData>();
        var validJson = JsonSerializer.Serialize(expectedData);

        var result = _parser.TryParse(validJson, out var data);

        result.Should().BeTrue();
        data.Should().NotBeNull();
        data.Should().BeEquivalentTo(expectedData);
    }

    [Fact]
    public void TryParse_ShouldReturnFalse_WhenInvalidJson()
    {
        const string invalidJson = "{ invalid json }";

        var result = _parser.TryParse(invalidJson, out var data);

        result.Should().BeFalse();
        data.Should().BeNull();
    }
}