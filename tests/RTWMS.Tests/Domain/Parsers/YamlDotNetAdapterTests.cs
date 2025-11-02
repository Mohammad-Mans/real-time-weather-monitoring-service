using AutoFixture;
using RTWMS.Domain.Adapters;
using RTWMS.Domain.Models;

namespace RTWMS.Tests.Domain.Parsers;

public class YamlDotNetAdapterTests
{
    private readonly YamlDotNetAdapter _adapter = new();

    [Fact]
    public void TryParse_ShouldReturnTrue_WhenValidYaml()
    {
        var fixture = new Fixture();
        var expectedData = fixture.Create<WeatherData>();
        var validYaml = $"{{Location: {expectedData.Location}, Temperature: {expectedData.Temperature}, Humidity: {expectedData.Humidity}}}";

        var result = _adapter.TryParse(validYaml, out var data);

        result.Should().BeTrue();
        data.Should().NotBeNull();
        data.Should().BeEquivalentTo(expectedData);
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