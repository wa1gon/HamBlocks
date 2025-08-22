namespace HamBlocks.Library.Models;


public record CallBookConf// : ICallBookConf
{
    [Key]
    public Guid Id { get; set; } = Guid.Empty; 
    public string Name { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string? UserName { get; set; } 
    public string? Password { get; set; }
    public string? ApiKey { get; set; } 
    public Guid HBConfigurationId { get; set; } = Guid.Empty; // Foreign key to LogConfig
    [NotMapped]
    public bool isDirty { get; set; } = false;
}
