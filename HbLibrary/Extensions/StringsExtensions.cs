namespace HbLibrary.Extensions;
/// <summary>
/// This is a zero allocation string compare method.
/// </summary>
public static class StringsExtensions
{
    public static bool Cmp(this string source, string target, bool ignoreCase = true, CultureInfo? culture = null)
    {
        if (ReferenceEquals(source, target)) return true;

        // One is null, the other isn't
        if (source is null || target is null) return false;
        
        if (culture is null)
        {
            var comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase
                : StringComparison.Ordinal;
            return string.Equals(source, target, comparison);     
        }
        else
        {
            // Culture-aware path
            var options = ignoreCase ? CompareOptions.IgnoreCase
                : CompareOptions.None;

            // If you want to also ignore accents/diacritics, OR in:
            // options |= CompareOptions.IgnoreNonSpace;

            return culture.CompareInfo.Compare(source, target, options) == 0;           
        }
    }
}
