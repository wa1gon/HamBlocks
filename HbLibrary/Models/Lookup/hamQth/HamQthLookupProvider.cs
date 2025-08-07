using System.Xml.Serialization;

namespace HamBlocks.Library.Models.Lookup;
[XmlRoot("HamQTH", Namespace = "https://www.hamqth.com")]



public class HamQthLookupProvider(string _userName, string _password, HttpClient _client, string _programId = "HamBlocksLib" ) : ILookupProvider
{

    private string? _sessionKey;
    private DateTime _expired;
    
    public async Task<ICallSignInfo?> LookupCallSignAsync(string callSign)
    {
        await LoginAsync();
        var url = $"https://www.hamqth.com/xml.php?id={_sessionKey}&callsign={callSign}&prg={_programId}";
        var xml = await _client.GetStringAsync(url);

        var serializer = new XmlSerializer(typeof(HamQthCallSearchResponse));
        using var reader = new StringReader(xml);
        var callValue =  serializer.Deserialize(reader) as HamQthCallSearchResponse;
        return ConvertToICallSignInfo(callValue);
    }

    private ICallSignInfo? ConvertToICallSignInfo(HamQthCallSearchResponse? callValue)
    {
        if (callValue is null)
            return null;

        var callrc = new CallSignInfo
        {
            CallSign = callValue.Search?.CallSign ?? string.Empty,
            Name = callValue.Search?.Nick ?? string.Empty,
            Country = callValue.Search?.Country ?? string.Empty,
            Grid = callValue.Search?.Grid ?? string.Empty,
            Dxcc = callValue.Search?.Adif ?? 0,
            State = callValue.Search?.UsState ?? string.Empty,
            County = callValue.Search?.UsCounty ?? string.Empty,
            Itu = callValue.Search?.Itu ?? 0,
            Cq = callValue.Search?.Cq ?? 0
        };
        return callrc;
    }

    private async Task LoginAsync()
    {
        if (_sessionKey != null && DateTime.UtcNow < _expired)
            return;
        
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
