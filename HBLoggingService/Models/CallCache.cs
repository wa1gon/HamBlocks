namespace HBLoggingService.Models;

public class CallCache
{
    public required string Call { get; set; }
    public string Grid { get; set; } = string.Empty;
    public DateTime AddedToCache { get; set; }
    public int Itu { get; set; } = 0;
    public string Continent { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public int Dxcc { get; set; } = 0;
}