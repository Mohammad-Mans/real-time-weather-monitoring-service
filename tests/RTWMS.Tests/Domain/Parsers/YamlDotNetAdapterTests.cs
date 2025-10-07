using RTWMS.Domain.Adapters;

namespace RTWMS.Tests.Domain.Parsers;

public class YamlDotNetAdapterTests
{
    private readonly YamlDotNetAdapter _adapter = new();

    [Fact]
    public void TryParse_ShouldReturnTrue_WhenValidYaml()
    {
        const string validYaml = "{Location: Test City, Temperature: 25.5, Humidity: 60.0}";

        var result = _adapter.TryParse(validYaml, out var data);

        result.Should().BeTrue();
        data.Should().NotBeNull();
        data!.Location.Should().Be("Test City");
        data.Temperature.Should().Be(25.5);
        data.Humidity.Should().Be(60.0);
    }

    [Fact]
    public void TryParse_ShouldReturnFalse_WhenInvalidYaml()
    {
        const string invalidYaml = "invalid: yaml: structure: [";

        var result = _adapter.TryParse(invalidYaml, out var data);

        result.Should().BeFalse();
        data.Should().BeNull();
    }
}