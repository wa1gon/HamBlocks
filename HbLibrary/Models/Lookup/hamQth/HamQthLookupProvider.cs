using System.Xml.Serialization;
using HbLibrary;

namespace HamBlocks.Library.Models.Lookup;
[XmlRoot("HamQTH", Namespace = "https://www.hamqth.com")]

    public class HamQthLookupProvider
        (string _userName,string _password, HttpClient _client, string _programId,
         IMemoryCache _cache): ILookupProvider
    {
        // private readonly string _userName;
        // private readonly string _password;
        // private readonly HttpClient _client;
        // private readonly string _programId;
        static public string? _sessionKey;
        static public DateTime _expired;


    public async Task<IDxccInfo?> LookupDxccAsync(int dxcc)
    {

        return null;
    }
    public async Task<ICallSignInfo?> LookupCallSignAsync(string callSign)
    {
        if (_cache.TryGetValue(callSign, out ICallSignInfo? cached))
        {
            Console.WriteLine("Cache hit for call sign: " + callSign);
            return cached;
        }

        await LoginAsync();
        var url = $"https://www.hamqth.com/xml.php?id={_sessionKey}&callsign={callSign}&prg={_programId}";
        var xml = await _client.GetStringAsync(url);

        var serializer = new XmlSerializer(typeof(HamQthCallSearchResponse));
        using var reader = new StringReader(xml);
        var callValue =  serializer.Deserialize(reader) as HamQthCallSearchResponse;
        var rc = ConvertToICallSignInfo(callValue);
        if (rc is not null)
        {
            _cache.Set(callSign, rc, TimeSpan.FromHours(1));
            Console.WriteLine("Cache set for call sign: " + callSign);
        }
        return rc;
    }
    public async Task<IDxccInfo?> LookupDxccByCallAsync(string callSign)
    {
        // await LoginAsync();
        var url = $"https://www.hamqth.com/dxcc.php?callsign={callSign}";
        var xml = await _client.GetStringAsync(url);

        var serializer = new XmlSerializer(typeof(HamQthDxccResponse));
        using var reader = new StringReader(xml);
        var dxccValue = serializer.Deserialize(reader) as HamQthDxccResponse;
        return ConvertToIDxccInfo(dxccValue?.Dxcc);
    }
    private IDxccInfo? ConvertToIDxccInfo(HamQthDxcc? dxcc)
    {
        if (dxcc is null)
            return null;

        var rc = new DxccInfo
        {
            CallSign = dxcc.CallSign ?? string.Empty,
            Name = dxcc.Name ?? string.Empty,
            Details = dxcc.Details ?? string.Empty,
            Continent = dxcc.Continent ?? string.Empty,
            Utc = dxcc.Utc ?? string.Empty,
            Waz = dxcc.Waz ?? 0,
            Itu = dxcc.Itu ?? 0,
            Dxcc = dxcc.Dxcc ?? 0
        };
        return rc;
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

    public async Task LoginAsync()
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
