namespace HamBlocks.Library.Models;



public record RigCtlConf //: IRigCtlConf
{
    [Key]
    public Guid Id { get; set; } = Guid.Empty; 
    public string Name { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public Guid HBConfigurationId { get; set; } = Guid.Empty; // Foreign key to LogConfig
    [NotMapped]
    public bool isDirty { get; set; } = false;
}
