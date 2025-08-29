namespace HamBlocks.Library.Models.Lookup;

[XmlRoot("HamQTH", Namespace = "http://www.hamqth.com")]
public class HamQthDxccResponse
{
    [XmlElement("dxcc")] public HamQthDxcc? Dxcc { get; set; }
}