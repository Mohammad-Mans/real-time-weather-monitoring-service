using System.Text.Json;
using AutoFixture;
using RTWMS.Domain.Models;
using RTWMS.Utils;

namespace RTWMS.Tests.Utils;

public class ConfigurationReaderTests : IDisposable
{
    private readonly string _testConfigPath;
    private readonly string _testConfigDirectory;

    public ConfigurationReaderTests()
    {
        _testConfigDirectory = Path.Combine(Path.GetTempPath(), "RTWMS_Test_Config");
        _testConfigPath = Path.Combine(_testConfigDirectory, "bot-settings.json");

        Directory.CreateDirectory(_testConfigDirectory);
    }

    public void Dispose()
    {
        if (Directory.Exists(_testConfigDirectory))
        {
            Directory.Delete(_testConfigDirectory, true);
        }
    }

    [Fact]
    public void ReadBotConfiguration_ShouldReturnValidConfig_WhenValidJsonFileExists()
    {
        var fixture = new Fixture();
        var expectedConfig = fixture.Create<AllBotsConfigs>();
        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };
        var validJson = JsonSerializer.Serialize(expectedConfig, jsonOptions);

        File.WriteAllText(_testConfigPath, validJson);

        var result = ConfigurationReader.ReadBotConfiguration(_testConfigPath);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedConfig);
    }

    [Fact]
    public void ReadBotConfiguration_ShouldReturnDefaultConfig_WhenFileDoesNotExist()
    {
        if (File.Exists(_testConfigPath))
            File.Delete(_testConfigPath);

        var result = ConfigurationReader.ReadBotConfiguration(_testConfigPath);

        result.Should().NotBeNull();
        result.Should().BeOfType<AllBotsConfigs>();
    }

    [Fact]
    public void ReadBotConfiguration_ShouldReturnDefaultConfig_WhenInvalidJsonInFile()
    {
        var invalidJson = "{ invalid json format }";
        File.WriteAllText(_testConfigPath, invalidJson);

        var result = ConfigurationReader.ReadBotConfiguration(_testConfigPath);

        result.Should().NotBeNull();
        result.Should().BeOfType<AllBotsConfigs>();
    }
}