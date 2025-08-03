using HBAbstractions;

namespace HbLibrary.RigControl;

public class HamLibRigCtlClient(string _host, int _port) : IDisposable, IRigControlClient
{
    
    private TcpClient? _client;
    private NetworkStream? _stream;
    private long _freq;

    public long Freq => _freq;

    public async Task OpenAsync()
    {
        try
        {
            _client = new TcpClient();
            await _client.ConnectAsync(_host, _port);
            _stream = _client.GetStream();
        }
        catch (Exception ex)
        {
            throw new IOException("Unable to connect to rigctld.", ex);
        }
    }

    public void Close()
    {
        _stream?.Dispose();
        _client?.Close();
        _client = null;
        _stream = null;
    }

    public async Task SetFreqAsync(long freq)
    {
        EnsureConnected();
        await SendCommandAsync($"F {freq}\n");
        _freq = freq;
    }

    public async Task SetModeAsync(string mode)
    {
        EnsureConnected();
        await SendCommandAsync($"M {mode}\n");
    }

    public async Task SendCommandAsync(string command)
    {
        if (_stream == null)
            throw new InvalidOperationException("Not connected to rigctld.");

        var buffer = System.Text.Encoding.ASCII.GetBytes(command);
        try
        {
            await _stream.WriteAsync(buffer, 0, buffer.Length);
            await _stream.FlushAsync();
        }
        catch (Exception ex)
        {
            throw new IOException("Lost connection to rigctld.", ex);
        }
    }

    private void EnsureConnected()
    {
        if (_client == null || !_client.Connected || _stream == null)
            throw new InvalidOperationException("Not connected to rigctld.");
    }

    public void Dispose()
    {
        Close();
    }
}   
