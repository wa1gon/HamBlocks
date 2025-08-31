namespace HBLoggingService.Requests;

public class QsoFilterRequest
{
    public string? Call { get; set; }

    // public DateTime? Date { get; set; }
    public int PageNumber { get; set; } = 1; // Default to the first page
    public int PageSize { get; set; } = 10; // Default page size
}