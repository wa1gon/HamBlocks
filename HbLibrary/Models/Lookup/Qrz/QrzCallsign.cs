namespace HamBlocks.Library.Models.Lookup.Qrz;

public class QrzCallsign
{
    [XmlElement("call")]
    public string? Call { get; set; }

    [XmlElement("aliases")]
    public string? Aliases { get; set; }

    [XmlElement("dxcc")]
    public int? Dxcc { get; set; }

    [XmlElement("fname")]
    public string? FirstName { get; set; }

    [XmlElement("name")]
    public string? LastName { get; set; }

    [XmlElement("addr1")]
    public string? Address1 { get; set; }

    [XmlElement("addr2")]
    public string? Address2 { get; set; }

    [XmlElement("state")]
    public string? State { get; set; }

    [XmlElement("zip")]
    public string? Zip { get; set; }

    [XmlElement("country")]
    public string? Country { get; set; }

    [XmlElement("ccode")]
    public int? CountryCode { get; set; }

    [XmlElement("lat")]
    public double? Latitude { get; set; }

    [XmlElement("lon")]
    public double? Longitude { get; set; }

    [XmlElement("grid")]
    public string? Grid { get; set; }

    [XmlElement("county")]
    public string? County { get; set; }

    [XmlElement("fips")]
    public string? Fips { get; set; }

    [XmlElement("land")]
    public string? Land { get; set; }

    [XmlElement("efdate")]
    public string? EffectiveDate { get; set; }

    [XmlElement("expdate")]
    public string? ExpiryDate { get; set; }

    [XmlElement("p_call")]
    public string? PreviousCall { get; set; }

    [XmlElement("class")]
    public string? LicenseClass { get; set; }

    [XmlElement("codes")]
    public string? Codes { get; set; }

    [XmlElement("qslmgr")]
    public string? QslManager { get; set; }

    [XmlElement("email")]
    public string? Email { get; set; }

    [XmlElement("url")]
    public string? Url { get; set; }

    [XmlElement("u_views")]
    public int? UserViews { get; set; }

    [XmlElement("bio")]
    public string? Bio { get; set; }

    [XmlElement("image")]
    public string? Image { get; set; }

    [XmlElement("serial")]
    public int? Serial { get; set; }

    [XmlElement("moddate")]
    public string? ModifiedDate { get; set; }

    [XmlElement("MSA")]
    public string? Msa { get; set; }

    [XmlElement("AreaCode")]
    public string? AreaCode { get; set; }

    [XmlElement("TimeZone")]
    public string? TimeZone { get; set; }

    [XmlElement("GMTOffset")]
    public int? GmtOffset { get; set; }

    [XmlElement("DST")]
    public string? Dst { get; set; }

    [XmlElement("eqsl")]
    public string? Eqsl { get; set; }

    [XmlElement("mqsl")]
    public string? Mqsl { get; set; }

    [XmlElement("cqzone")]
    public int? CqZone { get; set; }

    [XmlElement("ituzone")]
    public int? ItuZone { get; set; }

    [XmlElement("geoloc")]
    public string? GeoLoc { get; set; }

    [XmlElement("attn")]
    public string? Attn { get; set; }

    [XmlElement("nickname")]
    public string? Nickname { get; set; }

    [XmlElement("name_fmt")]
    public string? NameFormatted { get; set; }

    [XmlElement("born")]
    public int? Born { get; set; }
}
