namespace HBLoggingService.Requests;

public class GetQsosRequest
{
    public string? Call { get; set; }
    public bool IncludeDetails { get; set; } = false;
    public int Take { get; set; } = 0;
    public int Skip { get; set; } = 0;
}