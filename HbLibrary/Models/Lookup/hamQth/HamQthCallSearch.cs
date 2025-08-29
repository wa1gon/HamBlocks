namespace HamBlocks.Library.Models.Lookup;

public class HamQthCallSearch
{
    [XmlElement("callsign")] public string? CallSign { get; set; }

    [XmlElement("nick")] public string? Nick { get; set; }

    [XmlElement("qth")] public string? Qth { get; set; }

    [XmlElement("country")] public string? Country { get; set; }

    [XmlElement("adif")] public int? Adif { get; set; }

    [XmlElement("itu")] public int? Itu { get; set; }

    [XmlElement("cq")] public int? Cq { get; set; }

    [XmlElement("grid")] public string? Grid { get; set; }

    [XmlElement("adr_name")] public string? AdrName { get; set; }

    [XmlElement("adr_street1")] public string? AdrStreet1 { get; set; }

    [XmlElement("adr_city")] public string? AdrCity { get; set; }

    [XmlElement("adr_zip")] public string? AdrZip { get; set; }

    [XmlElement("adr_country")] public string? AdrCountry { get; set; }

    [XmlElement("adr_adif")] public int? AdrAdif { get; set; }

    [XmlElement("district")] public string? District { get; set; }

    [XmlElement("lotw")] public string? Lotw { get; set; }

    [XmlElement("qsl")] public string? Qsl { get; set; }

    [XmlElement("qsldirect")] public string? QslDirect { get; set; }

    [XmlElement("eqsl")] public string? Eqsl { get; set; }

    [XmlElement("email")] public string? Email { get; set; }

    [XmlElement("jabber")] public string? Jabber { get; set; }

    [XmlElement("skype")] public string? Skype { get; set; }

    [XmlElement("birth_year")] public int? BirthYear { get; set; }

    [XmlElement("lic_year")] public int? LicYear { get; set; }

    [XmlElement("web")] public string? Web { get; set; }

    [XmlElement("latitude")] public double? Latitude { get; set; }

    [XmlElement("longitude")] public double? Longitude { get; set; }

    [XmlElement("continent")] public string? Continent { get; set; }

    [XmlElement("utc_offset")] public int? UtcOffset { get; set; }

    [XmlElement("us_state")] public string? UsState { get; set; }

    [XmlElement("us_county")] public string? UsCounty { get; set; }
}