namespace HbLibrary.Extensions;

public static class QsoExtensions
{
    // List of fields that belong to the main Qso entity
    private static readonly HashSet<string> CoreQsoFields = new(StringComparer.OrdinalIgnoreCase)
    {
        nameof(Qso.Call),
        nameof(Qso.QsoDate),
        nameof(Qso.Mode),
        nameof(Qso.Freq),
        nameof(Qso.RstSent),
        nameof(Qso.RstRcvd),
        nameof(Qso.Band),
        nameof(Qso.MyCall)
    };

    /// <summary>
    ///     Adds a QsoDetail to the QSO only if the field is not part of the main Qso fields.
    /// </summary>
    public static bool TryAddDetail(this Qso qso, string fieldName, string fieldValue)
    {
        if (string.IsNullOrWhiteSpace(fieldName))
            return false;

        if (CoreQsoFields.Contains(fieldName))
            // Do not allow duplication of core fields
            return false;

        // Avoid duplicate fields within details
        if (qso.Details.Any(d => d.FieldName.Equals(fieldName, StringComparison.OrdinalIgnoreCase))) return false;

        qso.Details.Add(new QsoDetail
        {
            FieldName = fieldName,
            FieldValue = fieldValue
        });

        return true;
    }
}