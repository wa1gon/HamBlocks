namespace HBLoggingService.Options;

public class StationConfig
{
    public string? StationName { get; set; }
    public required string StationCallsign { get; set; }
    public string? StationLocation { get; set; }
    public required string StationGridSquare { get; set; }
}
