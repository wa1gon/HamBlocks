namespace HamBlocks.Library.Models;

public class QsoDetail
{
    [Key]
    public int Id { get; set; }
    public Guid QsoId { get; set; }
    [MaxLength(30)] [Required]
    public string FieldName { get; set; }  // e.g., "PotaRef", "SatMode", "QslVia"
    [MaxLength(255)] [Required]
    public string FieldValue { get; set; }
    public Qso Qso { get; set; } = new Qso();
}
