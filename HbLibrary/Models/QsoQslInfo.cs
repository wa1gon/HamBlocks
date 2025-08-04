namespace HamBlocks.Library.Models;

public class QsoQslInfo
{
    [Key]
    public int Id { get; set; }

    [MaxLength(20)]
    public required string ServiceName { get; set; }

    public bool QslSent { get; set; } = false;
    public DateTime? QslSentDate { get; set; }

    public bool QslReceived { get; set; } = false;
    public DateTime? QslReceivedDate { get; set; }

    [MaxLength(10)]
    public string? QslVia { get; set; }

    // Foreign key to Qso (Guid)
    public Guid QsoId { get; set; }
    [ForeignKey(nameof(QsoId))]
    [JsonIgnore]
    public Qso Qso { get; set; } = null!;
}
