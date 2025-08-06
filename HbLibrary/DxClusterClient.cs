namespace HbLibrary;
// https://www.ng3k.com/Misc/cluster.html
public class DxClusterClient(string _address, int _port)
{
    private TcpClient? _client;
    private StreamReader? _reader;

    public async Task ConnectAsync(string callsign)
    {
        _client = new TcpClient();
        await _client.ConnectAsync(_address, _port);
        var stream = _client.GetStream();
        _reader = new StreamReader(stream);
        var writer = new StreamWriter(stream) { AutoFlush = true };

        if (_client is null || _reader is null)
            throw new IOException("Unable to connect to DxCluster server.");

        var buffer = new StringBuilder();
        while (true)
        {
            var bufferChar = new char[1];
            int read = await _reader.ReadAsync(bufferChar, 0, 1);
            if (read == 0) throw new IOException("Connection closed before login.");
            buffer.Append(bufferChar[0]);

            var text = buffer.ToString().ToLower();
            if (text.Contains("login") || text.Contains("call"))
            {
                await writer.WriteLineAsync(callsign);
                break;
            }
        }
    }

    public async IAsyncEnumerable<DxSpot> GetSpotsAsync()
    {
        if (_reader == null)
            yield break;
        while (!_reader.EndOfStream)
        {
            var line = await _reader.ReadLineAsync();
            if (line == null) continue;
            var spot = ParseSpot(line);
            if (spot != null)
                yield return spot;
        }
    }

    private DxSpot? ParseSpot(string line)
    {
        // Example spot line: "14074.0 K1ABC FT8 CQ NA"
        var parts = line.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2) return null;

        if (!double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var freq))
            return null;

        return new DxSpot
        {
            Frequency = freq,
            Callsign = parts[1],
            Info = parts.Length > 2 ? parts[2] : "",
            Timestamp = DateTime.UtcNow
        };
    }
}
