

namespace HamBlocks.Library.Models.Lookup;

public class HamQthLookupProvider : ILookupProvider
{
    private readonly string _username;
    private readonly string _password;
    private string? _sessionKey;

    public HamQthLookupProvider(string username, string password)
    {
        _username = username;
        _password = password;
    }

    public async Task<ICallSignInfo?> LookupAsync(string callSign)
    {
        if (_sessionKey == null)
            await LoginAsync();

        using var client = new HttpClient();
        var url = $"https://www.hamqth.com/xml.php?id={_sessionKey}&callsign={callSign}";
        var xml = await client.GetStringAsync(url);
        var doc = XDocument.Parse(xml);

        var search = doc.Root?.Element("search");
        if (search == null) return null;

        return new CallSignInfo
        {
            CallSign = search.Element("call")?.Value ?? "",
            Name = search.Element("nick")?.Value ?? "",
            Country = search.Element("country")?.Value ?? "",
            Qth = search.Element("qth")?.Value ?? ""
        };
    }

    private async Task LoginAsync()
    {
        using var client = new HttpClient();
        var url = $"https://www.hamqth.com/xml.php?u={_username}&p={_password}";
        var xml = await client.GetStringAsync(url);
        var doc = XDocument.Parse(xml);
        _sessionKey = doc.Root?.Element("session")?.Element("session_id")?.Value;
        if (string.IsNullOrEmpty(_sessionKey))
            throw new Exception("HamQTH login failed.");
    }
}
