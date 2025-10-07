using RTWMS.Domain.Parsers;

namespace RTWMS.Tests.Domain.Parsers;

public class JsonDataParserTests
{
    private readonly JsonDataParser _parser = new();

    [Fact]
    public void TryParse_ShouldReturnTrue_WhenValidJson()
    {
        const string validJson = """{"Location": "Test City","Temperature": 25.5,"Humidity": 60.0}""";

        var result = _parser.TryParse(validJson, out var data);

        result.Should().BeTrue();
        data.Should().NotBeNull();
        data.Location.Should().Be("Test City");
        data.Temperature.Should().Be(25.5);
        data.Humidity.Should().Be(60.0);
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