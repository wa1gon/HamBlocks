namespace HbLibrary;

// https://www.ng3k.com/Misc/cluster.html
public class DxClusterClient(string _address, int _port)
{
    private TcpClient? _client;
    private StreamReader? _reader;

    public async IAsyncEnumerable<string> ReadRawLinesAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (_reader == null)
            yield break;
        while (!cancellationToken.IsCancellationRequested && !_reader.EndOfStream)
        {
            var line = await _reader.ReadLineAsync();
            if (line != null)
                yield return line;
        }
    }

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
            var read = await _reader.ReadAsync(bufferChar, 0, 1);
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
        Console.WriteLine("Top of while loop in GetSpotsAsync");
        while (!_reader.EndOfStream)
        {
            var line = await _reader.ReadLineAsync();
            if (line == null) continue;
            Console.WriteLine($"Raw line: {line}");
            var spot = ParseSpot(line);
            if (spot != null)
                yield return spot;
        }
    }

    private DxSpot? ParseSpot(string line)
    {
        if (!line.StartsWith("DX de ")) return null;

        var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 6) return null;

        var spotter = parts[2].TrimEnd(':');
        if (!double.TryParse(parts[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var freq))
            return null;

        return new DxSpot
        {
            Spotter = spotter,
            Frequency = freq,
            Callsign = parts[4],
            Info = string.Join(' ', parts.Skip(5).Take(parts.Length - 6)),
            Timestamp = DateTime.UtcNow
        };
    }
}