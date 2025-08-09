namespace HamBlocks.Library.Models;



public class RigCtlConf : IRigCtlConf
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
}
