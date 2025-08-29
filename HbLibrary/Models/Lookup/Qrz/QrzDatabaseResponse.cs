namespace HamBlocks.Library.Models.Lookup.Qrz;

[XmlRoot("QRZDatabase", Namespace = "http://xmldata.qrz.com")]
public class QrzDatabaseResponse
{
    [XmlElement("Session")] public QrzSession? Session { get; set; }

    [XmlElement("Callsign")] public QrzCallsign? Callsign { get; set; }
}