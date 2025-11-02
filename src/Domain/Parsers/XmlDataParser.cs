using System.Xml;
using RTWMS.Domain.Interfaces;
using RTWMS.Domain.Models;

namespace RTWMS.Domain.Parsers;

public sealed class XmlDataParser : IDataParser
{
    public bool TryParse(string input, out WeatherData? data)
    {
        try
        {
            var document = new XmlDocument();
            document.LoadXml(input);

            var location = document.SelectSingleNode("//Location")?.InnerText ?? string.Empty;
            var temperature = double.Parse(document.SelectSingleNode("//Temperature")?.InnerText ?? "0");
            var humidity = double.Parse(document.SelectSingleNode("//Humidity")?.InnerText ?? "0");

            data = new WeatherData
            {
                Location = location,
                Temperature = temperature,
                Humidity = humidity
            };
            return true;
        }
        catch (Exception)
        {
            data = null;
            return false;
        }
    }
}