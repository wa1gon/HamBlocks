using System.Xml.Serialization;

namespace HamBlocks.Library.Models.Lookup;
[XmlRoot("HamQTH", Namespace = "https://www.hamqth.com")]
public class HamQthResponse
{
    [XmlElement("session")]
    public Session? Session { get; set; }
}

public class Session
{
    [XmlElement("session_id")]
    public string? SessionId { get; set; }
}
public class HamQthLookupProvider(string _userName, string _password, HttpClient _client ) : ILookupProvider
{

    private string? _sessionKey;
    private DateTime _expired;
    
    public async Task<ICallSignInfo?> LookupAsync(string callSign)
    {
        if (_sessionKey == null || DateTime.UtcNow > _expired)
            await LoginAsync();
        
        var url = $"https://www.hamqth.com/xml.php?id={_sessionKey}&callsign={callSign}";
        var xml = await _client.GetStringAsync(url);
        var doc = XDocument.Parse(xml);

        var search = doc.Root?.Element("search");
        if (search == null) return null;

        return new CallSignInfo
        {
            CallSign = search.Element("call")?.Value ?? "",
            Name = search.Element("nick")?.Value ?? "",
            Country = search.Element("country")?.Value ?? "",
            Qth = search.Element("qth")?.Value ?? "",
            State = search.Element("us_state")?.Value ?? "",
            Grid = search.Element("grid")?.Value ?? "",
            County = search.Element("us_county")?.Value ?? "",
            Dxcc = int.TryParse(search.Element("adif")?.Value, out var dxcc) ? dxcc : 0,
            Itu = int.TryParse(search.Element("itu")?.Value, out var itu) ? dxcc : 0,
            Cq = int.TryParse(search.Element("cq")?.Value, out var cq) ? dxcc : 0,
        };
    }

    private async Task LoginAsync()
    {
        var url = $"https://www.hamqth.com/xml.php?u={_userName}&p={_password}";
        var xml = await _client.GetStringAsync(url);
        var doc = XDocument.Parse(xml);

        // Get the default namespace from the root element
        XNamespace ns = doc.Root?.GetDefaultNamespace() ?? "";

        var sessionId = doc.Root?
            .Element(ns + "session")?
            .Element(ns + "session_id")?
            .Value;

        if (string.IsNullOrEmpty(sessionId))
            throw new Exception("HamQTH login failed.");

        _sessionKey = sessionId;
        _expired = DateTime.UtcNow + TimeSpan.FromMinutes(59);
    }
}
