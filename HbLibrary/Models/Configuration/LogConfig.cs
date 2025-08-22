namespace HamBlocks.Library.Models;


public record LogConfig 
{
    [Key]
    public Guid Id { get; set; } = Guid.Empty; // Unique identifier for the configuration
    [MaxLength(50)]
    public required string ProfileName { get; set; } 
    [Callsign] [MaxLength(20)]
    public required string Callsign { get; set; }
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
    [NotMapped]
    public bool IsDirty { get; set; } = false; 
    public List<RigCtlConf> RigControls { get; set; } = [];
    public List<CallBookConf> Logbooks { get; set; } = [];
    public List<DxClusterConf> DxClusters { get; set; } = [];
    
    public LogConfig Copy()
    {
        return this with
        {
            Logbooks = Logbooks.Select(cb => cb with { }).ToList(),
            RigControls = RigControls.Select(rc => rc with { }).ToList(),
            DxClusters = DxClusters.Select(dc => dc with { }).ToList()

        };
    }
    
}
