namespace HamBlocks.Library.Models.Lookup.Qrz;

public class QrzLookupProvider(
    string userName,
    string password,
    HttpClient client,
    string programId,
    IMemoryCache cache) : ILookupProvider
{
    private string? _sessionKey;


    public async Task LoginAsync()
    {
        if (!string.IsNullOrEmpty(_sessionKey)) return;

        var url = $"https://xmldata.qrz.com/xml/current/?username={userName};password={password}";
        var xml = await client.GetStringAsync(url);
        var serializer = new XmlSerializer(typeof(QrzDatabaseResponse));
        using var reader = new StringReader(xml);
        var response = (QrzDatabaseResponse?)serializer.Deserialize(reader);
        _sessionKey = response?.Session?.Key ?? throw new InvalidOperationException("QRZ login failed");
    }

    public async Task<ICallSignInfo?> LookupCallSignAsync(string callSign)
    {
        await LoginAsync();
        var url = $"https://xmldata.qrz.com/xml/current/?s={_sessionKey};callsign={callSign}";
        var xml = await client.GetStringAsync(url);
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