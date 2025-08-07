namespace HamBlocks.Library.Models.Lookup;

public class HamQthDxcc
{
    [XmlElement("callsign")]
    public string? CallSign { get; set; }

    [XmlElement("name")]
    public string? Name { get; set; }

    [XmlElement("details")]
    public string? Details { get; set; }

    [XmlElement("continent")]
    public string? Continent { get; set; }

    [XmlElement("utc")]
    public string? Utc { get; set; }

    [XmlElement("waz")]
    public int? Waz { get; set; }

    [XmlElement("itu")]
    public int? Itu { get; set; }

    [XmlElement("lat")]
    public double? Lat { get; set; }

    [XmlElement("lng")]
    public double? Lng { get; set; }

    [XmlElement("adif")]
    public int? Dxcc { get; set; }
}
