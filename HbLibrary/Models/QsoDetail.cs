namespace HamBlocks.Library.Models;

public class QsoDetail
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(30)]
    public string FieldName { get; set; }  // e.g., "PotaRef", "SatMode", "QslVia"

    [MaxLength(255)]
    public string FieldValue { get; set; }

    public Qso Qso { get; set; }
}
