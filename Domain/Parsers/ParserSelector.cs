using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Adapters;

namespace RTWMS.Domain.Parsers;

public sealed class ParserSelector : IParserSelector
{
    private readonly IReadOnlyList<IDataParser> _parsers =
    [
        new JsonDataParser(),
        new XmlDataParser(),
        new YamlDotNetAdapter()
    ];

    public IDataParser? SelectParser(string input)
    {
        return _parsers.FirstOrDefault(p => p.TryParse(input, out _));
    }
}