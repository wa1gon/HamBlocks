namespace HamBlocks.Library.Models;

public class HBConfiguration
{
    [Key]
    public required string ProfileName { get; set; }
    public List<RigCtlConf> Rigs { get; set; } = [];
    public List<CallBookConf> Logbooks { get; set; } = [];
}
