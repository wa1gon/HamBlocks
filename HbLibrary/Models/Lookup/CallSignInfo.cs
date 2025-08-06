namespace HamBlocks.Library.Models.Lookup;

public class CallSignInfo: ICallSignInfo
{
    public string CallSign { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Qth { get; set; } = string.Empty;
    // Add more fields as needed
}
