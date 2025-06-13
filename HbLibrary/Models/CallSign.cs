namespace HamBlocks.Library.Models;

public record class CallSign
{
    [Required]
    public string Call { get; set; } = string.Empty;

    public bool IsPrimary { get; set; } = true;
    public Guid OperatorId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.MinValue;
    public DateTime EndDate { get; set; } = DateTime.MaxValue;
    public string Class { get; set; } = string.Empty;
}
