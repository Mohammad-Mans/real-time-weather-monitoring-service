using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Parsers;

namespace RTWMS.Domain.Factories;

public class ParserFactory : IParserFactory
{
    public IDataParser? GetParser(string input)
    {
        if (JsonDataParser.IsValidFormat(input))
            return new JsonDataParser();

        if (XmlDataParser.IsValidFormat(input))
            return new XmlDataParser();

        return null;
    }
}