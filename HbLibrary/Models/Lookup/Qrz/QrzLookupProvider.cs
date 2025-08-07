namespace HamBlocks.Library.Models.Lookup.Qrz;

public class QrzLookupProvider : ILookupProvider
{
    private readonly string _username;
    private readonly string _password;
    private readonly HttpClient _client;
    private string? _sessionKey;

    public QrzLookupProvider(string username, string password, HttpClient client)
    {
        _username = username;
        _password = password;
        _client = client;
    }

    public async Task LoginAsync()
    {
        if (!string.IsNullOrEmpty(_sessionKey)) return;

        var url = $"https://xmldata.qrz.com/xml/current/?username={_username};password={_password}";
        var xml = await _client.GetStringAsync(url);
        var serializer = new XmlSerializer(typeof(QrzDatabaseResponse));
        using var reader = new StringReader(xml);
        var response = (QrzDatabaseResponse?)serializer.Deserialize(reader);
        _sessionKey = response?.Session?.Key ?? throw new InvalidOperationException("QRZ login failed");
    }

    public async Task<ICallSignInfo?> LookupCallSignAsync(string callSign)
    {
        await LoginAsync();
        var url = $"https://xmldata.qrz.com/xml/current/?s={_sessionKey};callsign={callSign}";
        var xml = await _client.GetStringAsync(url);
        var serializer = new XmlSerializer(typeof(QrzDatabaseResponse));
        using var reader = new StringReader(xml);
        var response = (QrzDatabaseResponse?)serializer.Deserialize(reader);
        var cs = response?.Callsign;
        if (cs == null) return null;

        return new CallSignInfo
        {
            CallSign = cs.Call ?? "",
            Name = $"{cs.LastName}".Trim(),
            // LastName = cs.LastName ?? "",
            State = cs.State ?? "",
            Country = cs.Country ?? "",
            Grid = cs.Grid ?? ""
        };
    }
}
