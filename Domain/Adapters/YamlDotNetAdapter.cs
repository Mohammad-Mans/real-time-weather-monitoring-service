using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Core;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.Domain.Adapters;

public sealed class YamlDotNetAdapter : IDataParser
{
    private readonly IDeserializer _deserializer;

    public YamlDotNetAdapter()
    {
        _deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();
    }

    public bool TryParse(string input, out WeatherData? data)
    {
        data = null;

        try
        {
            data = _deserializer.Deserialize<WeatherData>(input);
            return true;
        }
        catch (YamlException)
        {
            data = null;
            return false;
        }
        catch (Exception)
        {
            data = null;
            return false;
        }
    }
}