using System.Text.Json;
using RTWMS.Domain.Models;

namespace RTWMS.Utils;

public class ConfigurationReader
{
    private static string GetConfigPath()
    {
        var parentPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString();
        if (parentPath == null) throw new Exception("Invalid File Path");
        return Path.Combine(parentPath, @"Configuration\bot-settings.json");
    }

    public static AllBotsConfigs ReadBotConfiguration()
    {
        try
        {
            var configPath = GetConfigPath();
            var jsonString = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<AllBotsConfigs>(jsonString);
            return config ?? new AllBotsConfigs();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading configuration file: {ex.Message}");
            return new AllBotsConfigs();
        }
    }
}