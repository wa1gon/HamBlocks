namespace HamBlocks.Library.Models;

public class HBConfiguration
{
    [Key][MaxLength(50)]
    public required string ProfileName { get; set; }
    [MaxLength(20)]
    public required string Callsign { get; set; } = string.Empty;
    [MaxLength(20)]
    public string StationName { get; set; } = string.Empty;
    [MaxLength(20)]
    public string GridSquare { get; set; } = string.Empty;
    [MaxLength(50)]
    public string City { get; set; } = string.Empty;
    [MaxLength(50)]
    public string County { get; set; } = string.Empty;
    [MaxLength(10)]
    public string CountyCode { get; set; } = string.Empty;
    [MaxLength(2)]
    public string State { get; set; } = string.Empty;
    public int Dxcc { get; set; } = 0; // DXCC Entity Code
    public int ProKey { get; set; } = 0; 
    
    public List<RigCtlConf> RigControls { get; set; } = [];
    public List<CallBookConf> Logbooks { get; set; } = [];
    public List<DxClusterConf> DxClusters { get; set; } = [];
}
