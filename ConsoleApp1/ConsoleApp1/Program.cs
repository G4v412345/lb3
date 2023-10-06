using System.Xml.Linq;

public class Log
{
    public List<Event> Events { get; set; } = new List<Event>(); 
}
public class Event
{
    public string date { get; set; } 
    public string res { get; set; }
    public string ipfrom { get; set; }
    public string url_to { get; set; }
    public string method { get; set; }
    public int response { get; set; }
}
class Program
{
    static void Main(string[] args)
    {
        string xml = @"<?xml version=""1.0"" ?>
                        <log>
                            <event date=""27/May/1999:02:32:46"" result=""success"">
                                <ip-from>195.151.62.18</ip-from>
                                <method>GET</method>
                                <url-to>/misc/</url-to>
                                <response>200</response>
                            </event>
                            <event date=""27/May/1999:02:41:47"" result=""success"">
                                <ip-from>195.209.248.12</ip-from>
                                <method>GET</method>
                                <url-to>/soft.htm</url-to>
                                <response>200</response>
                            </event>
                        </log>";
        Log log = parsexml(xml);
        foreach (var i in log.Events)
        {
            Console.WriteLine($"Date: {i.date}");
            Console.WriteLine($"Result: {i.res}");
            Console.WriteLine($"IP From: {i.ipfrom}");
            Console.WriteLine($"Method: {i.method}");
            Console.WriteLine($"URL To: {i.url_to}");
            Console.WriteLine($"Response: {i.response}");
            Console.WriteLine();
        }
    }
    static Log parsexml(string xml)
    { 
        XElement root = XElement.Parse(xml);
        Log log = new Log();
        foreach (XElement eventElement in root.Elements("event"))
        {
            Event evt = new Event
            {
                date = eventElement.Attribute("date").Value,
                res = eventElement.Attribute("result").Value,
                ipfrom = eventElement.Element("ip-from").Value,
                method = eventElement.Element("method").Value,
                url_to = eventElement.Element("url-to").Value,
                response = int.Parse(eventElement.Element("response").Value)
            };

            log.Events.Add(evt);
        }

        return log;
    }
}
