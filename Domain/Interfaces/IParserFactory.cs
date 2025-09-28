namespace RTWMS.Domain.Interfaces;

public interface IParserFactory
{
    IDataParser? GetParser(string input);
}