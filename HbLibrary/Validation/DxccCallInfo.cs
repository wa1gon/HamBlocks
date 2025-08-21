namespace HbLibrary;

public class DxccCallInfo
{
    public static DxccEntity? FindMatchingPrefix(string callSign, List<DxccEntity> entries)
    {
        callSign = callSign.ToUpper();
        foreach (var entry in entries)
        {
            if (string.IsNullOrEmpty(entry.PrefixRegex) )
            {
                continue; // Skip entries with empty PrefixRegex or callSign
            }
            if (Regex.IsMatch(callSign, entry.PrefixRegex))
            {
                return entry; // Stop at first match
            }
        }
        return null; // No matches found
    }

    public static bool IsValidCallSign(string callSign,List<DxccEntity> entries)
    {
        if (string.IsNullOrEmpty(callSign))
        {
            return false; // Empty or null callsign is invalid
        }

        // Check length
        if (callSign.Length < 2 || callSign.Length > 10)
        {
            return false; // Callsign must be between 2 and 10 characters
        }
        
        var dxccEntity = FindMatchingPrefix(callSign, entries);

        return true; // Valid callsign
    }
} 
