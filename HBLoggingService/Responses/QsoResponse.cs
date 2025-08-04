namespace HBLoggingService.Responses;

public class QsoResponse
{
    public Guid Id { get; set; }
    public required string Call { get; set; }
    public DateTime Date { get; set; }
    // Add other properties as needed
}
