namespace HamBlocks.Library.Models.Lookup;

public class CallSignInfo : ICallSignInfo
{
    public bool Lotw { get; set; }
    public bool Eqsl { get; set; }
    public string CallSign { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string County { get; set; } = string.Empty;
    public string Grid { get; set; } = string.Empty;
    public int Dxcc { get; set; }
    public int Itu { get; set; }
    public int Cq { get; set; }

    // Add more fields as needed
}