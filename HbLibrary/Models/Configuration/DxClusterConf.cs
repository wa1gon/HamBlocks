namespace HamBlocks.Library.Models;

public class DxClusterConf
{
    [Key]
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 7300; // Default port for DX Cluster
    public string? UserName { get; set; } // Optional username for authentication
    public string? Password { get; set; } // Optional password for authentication
}
