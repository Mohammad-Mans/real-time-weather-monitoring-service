namespace RTWMS.Domain.Models;

public class AllBotsConfigs
{
    public BotConfig RainBot { get; set; } = new();
    public BotConfig SunBot { get; set; } = new();
    public BotConfig SnowBot { get; set; } = new();
}