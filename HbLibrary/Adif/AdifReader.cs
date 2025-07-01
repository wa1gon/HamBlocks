
namespace HbLibrary.Adif;
public class AdifReader
{
    private static readonly Regex AdifFieldPattern = new(@"<([^:>]+):(\d+)(:[^>]*)?>([^<]*)", RegexOptions.IgnoreCase);

    public static List<Qso> ReadFromFile(string filePath)
    {
        string content = File.ReadAllText(filePath);

        // Skip to after the <EOH> tag
        int eohIndex = content.IndexOf("<EOH>", StringComparison.OrdinalIgnoreCase);
        if (eohIndex < 0)
            throw new InvalidDataException("Missing <EOH> tag.");

        string qsoBlock = content.Substring(eohIndex + 5);
        string[] records = Regex.Split(qsoBlock, "<EOR>", RegexOptions.IgnoreCase);

        var qsos = new List<Qso>();

        foreach (string record in records)
        {
            if (string.IsNullOrWhiteSpace(record)) continue;

            var fields = ParseFields(record);
            var qso = ParseQso(fields);
            qsos.Add(qso);
        }

        return qsos;
    }

    private static Dictionary<string, string> ParseFields(string record)
    {
        var fields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (Match match in AdifFieldPattern.Matches(record))
        {
            string fieldName = match.Groups[1].Value.Trim();
            int length = int.Parse(match.Groups[2].Value.Trim());
            string value = match.Groups[4].Value.Trim();

            if (value.Length > length)
                value = value.Substring(0, length);

            fields[fieldName] = value;
        }

        return fields;
    }

    private static Qso ParseQso(Dictionary<string, string> fields)
    {
        var qso = new Qso();
        LocalDate? date = null;
        LocalTime? time = null;

        foreach (var field in fields)
        {
            string name = field.Key.ToLowerInvariant();
            string value = field.Value;

            switch (name)
            {
                case "call": qso.Call = value; break;
                case "band": qso.Band = value; break;
                case "mode": qso.Mode = value; break;
                case "country": qso.Country = value; break;
                case "state": qso.State = value; break;
                case "rst_sent": qso.RstSent = value; break;
                case "rst_rcvd": qso.RstRcvd = value; break;
                case "contest-id": qso.ContestId = value; break;
                case "guid": qso.Id = Guid.Parse(value); break;
                case "freq":
                    if (decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var freq))
                        qso.Freq = freq;
                    break;

                case "qso_date":
                    var dateResult = LocalDatePattern.CreateWithInvariantCulture("yyyyMMdd").Parse(value);
                    if (dateResult.Success)
                        date = dateResult.Value;
                    break;

                case "time_on":
                    var timePattern = value.Length == 4
                        ? LocalTimePattern.CreateWithInvariantCulture("HHmm")
                        : LocalTimePattern.CreateWithInvariantCulture("HHmmss");

                    var timeResult = timePattern.Parse(value);
                    if (timeResult.Success)
                        time = timeResult.Value;
                    break;

                default:
                    // Store all non-core fields in QsoDetail
                    qso.Details.Add(new QsoDetail
                    {
                        FieldName = field.Key,
                        FieldValue = value
                    });
                    break;
            }
        }
        if (qso.Id == Guid.Empty)
        {
            qso.Id = Guid.NewGuid();
        }
        // Combine date + time into Instant
        if (date.HasValue)
        {
            var localTime = time ?? new LocalTime(0, 0);
            var localDateTime = date.Value + localTime;
            qso.QsoDate = localDateTime.InZoneStrictly(DateTimeZone.Utc).ToDateTimeUtc();
        }
        else
        {
            qso.QsoDate = DateTimeOffset.FromUnixTimeSeconds(0).UtcDateTime; // fallback if no date
        }

        return qso;
    }
}
