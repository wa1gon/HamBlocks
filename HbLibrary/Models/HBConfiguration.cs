namespace HamBlocks.Library.Models;

public class HBConfiguration
{
    public Guid Id { get; set; }
    public List<RigCtlConf> Rigs { get; set; } = [];
    public List<CallBookConf> Logbooks { get; set; } = [];
}

public class CallBookConf
{
}

public class RigCtlConf
{
}
