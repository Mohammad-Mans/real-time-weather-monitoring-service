namespace RTWMS.Domain.Interfaces;

public interface IParserSelector
{
    IDataParser? SelectParser(string input);
}