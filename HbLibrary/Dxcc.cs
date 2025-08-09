namespace HbLibrary;

public class Dxcc
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
} 
