using System.ComponentModel.DataAnnotations.Schema;

namespace HamBlocks.Library.Models;

public class QsoDetail
{
    [Key]
    public int Id { get; set; }
    public Guid QsoId { get; set; }
    [MaxLength(30)] [Required]
    public string FieldName { get; set; }  // e.g., "arrl_section", "class", "QslVia"
    
    [MaxLength(255)] [Required]
    public string FieldValue { get; set; }
    [ForeignKey(nameof(QsoId))]
    public Qso? Qso { get; set; } = null;
}
