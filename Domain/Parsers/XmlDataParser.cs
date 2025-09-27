using System.Xml;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.Domain.Parsers;

public class XmlDataParser : IDataParser
{
    public WeatherData? Parse(string input)
    {
        try
        {
            var document = new XmlDocument();
            document.LoadXml(input);

            var location = document.SelectSingleNode("//Location")?.InnerText ?? string.Empty;
            var temperature = double.Parse(document.SelectSingleNode("//Temperature")?.InnerText ?? "0");
            var humidity = double.Parse(document.SelectSingleNode("//Humidity")?.InnerText ?? "0");

            return new WeatherData
            {
                Location = location,
                Temperature = temperature,
                Humidity = humidity
            };
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static bool IsValidFormat(string input)
    {
        try
        {
            var document = new XmlDocument();
            document.LoadXml(input);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}