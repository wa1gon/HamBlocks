namespace HbLibrary.Extensions;
/// <summary>
/// This is a zero allocation string compare method.
/// </summary>
public static class StringsExtensions
{
    public static bool Cmp(this string source, string target, bool ignoreCase = true, CultureInfo? culture = null)
    {
        if (ReferenceEquals(source, target)) return true;
        if (source is null || target is null) return false;
        
        if (culture is null)
        {
            var comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase
                : StringComparison.Ordinal;
            return string.Equals(source, target, comparison);     
        }
        else
        {
            var options = ignoreCase ? CompareOptions.IgnoreCase
                : CompareOptions.None;
            return culture.CompareInfo.Compare(source, target, options) == 0;           
        }
    }
}
