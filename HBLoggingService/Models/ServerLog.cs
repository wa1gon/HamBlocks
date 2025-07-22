namespace HBLoggingService.Models;

public class ServerLog
{
    public Guid Id { get; set; }
    public DateTime Dtg { get; set; }
    public string Message { get; set; }
    public int TotalErrors { get; set; } = 0;

}
