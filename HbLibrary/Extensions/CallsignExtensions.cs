namespace HbLibrary.Extensions;

public static class CallsignExtensions
{
      private static readonly List<(string Prefix, string Country)> PrefixCountryList = new()
    {
        ("K", "United States"), ("N", "United States"), ("W", "United States"),
        ("AA", "United States"), ("AB", "United States"), ("AC", "United States"),
        ("VE", "Canada"), ("VA", "Canada"), ("VO", "Canada"), ("VY", "Canada"),
        ("G", "United Kingdom"), ("M", "United Kingdom"), ("2E", "United Kingdom"),
        ("JA", "Japan"), ("JE", "Japan"), ("JF", "Japan"), ("JG", "Japan"), ("JI", "Japan"),
        ("VK", "Australia"), ("ZL", "New Zealand"), ("DL", "Germany"), ("F", "France"),
        ("I", "Italy"), ("EA", "Spain"), ("CT", "Portugal"), ("OE", "Austria"),
        ("ON", "Belgium"), ("PA", "Netherlands"), ("SM", "Sweden"), ("OH", "Finland"),
        ("LA", "Norway"), ("OZ", "Denmark"), ("LX", "Luxembourg"), ("HB", "Switzerland"),
        ("YU", "Serbia"), ("LZ", "Bulgaria"), ("YO", "Romania"), ("HA", "Hungary"),
        ("SP", "Poland"), ("OK", "Czech Republic"), ("OM", "Slovakia"),
        ("S5", "Slovenia"), ("9A", "Croatia"), ("SV", "Greece"), ("SU", "Egypt"),
        ("ZS", "South Africa"), ("V5", "Namibia"), ("5H", "Tanzania"), ("5Z", "Kenya"),
        ("4X", "Israel"), ("A4", "Oman"), ("A6", "United Arab Emirates"),
        ("AP", "Pakistan"), ("VU", "India"), ("HS", "Thailand"), ("HL", "South Korea"),
        ("BV", "Taiwan"), ("BY", "China"), ("VR", "Hong Kong"), ("DU", "Philippines"),
        ("YB", "Indonesia"), ("9V", "Singapore"), ("9M", "Malaysia"),
        ("KH6", "Hawaii"), ("KH2", "Guam"), ("KP4", "Puerto Rico"),
        ("TG", "Guatemala"), ("TI", "Costa Rica"), ("HP", "Panama"),
        ("PY", "Brazil"), ("LU", "Argentina"), ("CX", "Uruguay"),
        ("CE", "Chile"), ("OA", "Peru"), ("HC", "Ecuador"),
        ("YV", "Venezuela"), ("HK", "Colombia"), ("TG", "Guatemala"),
        ("XE", "Mexico"), ("CO", "Cuba"), ("CM", "Cuba"),
        // Add more as needed for full ITU coverage
    };

    public static string GetCountryFromCallSign(this string callSign)
    {
        if (string.IsNullOrWhiteSpace(callSign))
            return "Unknown";

        callSign = callSign.ToUpperInvariant();

        var match = PrefixCountryList
            .OrderByDescending(pc => pc.Prefix.Length)
            .FirstOrDefault(pc => callSign.StartsWith(pc.Prefix));

        return string.IsNullOrEmpty(match.Country) ? "Unknown" : match.Country;
    }  
}
