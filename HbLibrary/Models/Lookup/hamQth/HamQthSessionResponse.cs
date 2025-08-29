namespace HamBlocks.Library.Models.Lookup;

public class HamQthSessionResponse
{
    [XmlElement("session")] public Session? Session { get; set; }
}