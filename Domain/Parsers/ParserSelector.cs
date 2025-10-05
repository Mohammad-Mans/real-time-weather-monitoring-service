using RTWMS.Domain.Interfaces;

namespace RTWMS.Domain.Parsers;

public sealed class ParserSelector : IParserSelector
{
    private readonly IReadOnlyList<IDataParser> _parsers =
    [
        new JsonDataParser(),
        new XmlDataParser()
    ];

    public IDataParser? SelectParser(string input)
    {
        return _parsers.FirstOrDefault(p => p.TryParse(input, out _));
    }
}