namespace HamBlocks.Library.Models.Lookup;

[XmlRoot("HamQTH", Namespace = "https://www.hamqth.com")]
public class HamQthCallSearchResponse
{
    [XmlElement("search")] public HamQthCallSearch? Search { get; set; }
}