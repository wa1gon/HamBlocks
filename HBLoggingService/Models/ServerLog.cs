

namespace HBLoggingService.Models;

public class ServerLog
{
    public Guid Id { get; set; }
    public DateTime Dtg { get; set; }
    [MaxLength(255)] [Required]
    public required string Message { get; set; }
    public int TotalErrors { get; set; } = 0;

}
