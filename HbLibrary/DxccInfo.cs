namespace HbLibrary;

public class DxccInfo : IDxccInfo
{
    public string CallSign { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public string Continent { get; set; }  = string.Empty;
    public string Utc { get; set; } = string.Empty;
    public int Waz { get; set; }
    public int Dxcc { get; set; }
    public int Itu { get; set; }
    public int Cq { get; set; }
}
