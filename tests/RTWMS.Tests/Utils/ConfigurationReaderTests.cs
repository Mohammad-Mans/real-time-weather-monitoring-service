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
        var validJson =
            """
            {
                "RainBot": {
                    "Enabled": true,
                    "HumidityThreshold": 80.0,
                    "TemperatureThreshold": 20.0,
                    "Message": "It looks like it's about to pour down!"
                },
                "SunBot": {
                    "Enabled": true,
                    "HumidityThreshold": 40.0,
                    "TemperatureThreshold": 30.0,
                    "Message": "Wow, it's a scorcher out there!"
                }
            }
            """;

        File.WriteAllText(_testConfigPath, validJson);

        var result = ConfigurationReader.ReadBotConfiguration(_testConfigPath);

        result.Should().NotBeNull();
        result.RainBot.Enabled.Should().BeTrue();
        result.RainBot.HumidityThreshold.Should().Be(80.0);
        result.RainBot.Message.Should().Be("It looks like it's about to pour down!");

        result.SunBot.Enabled.Should().BeTrue();
        result.SunBot.TemperatureThreshold.Should().Be(30.0);
        result.SunBot.Message.Should().Be("Wow, it's a scorcher out there!");
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