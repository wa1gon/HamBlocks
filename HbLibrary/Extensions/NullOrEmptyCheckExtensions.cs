using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic;

namespace HbLibrary.Extensions;

public static class NullOrEmptyCheckExtensions
{
    public static bool IsNotEmptyOrNull(this string? str)
    {
        return !string.IsNullOrEmpty(str);
    }

    public static bool IsNullOrEmpty(this IEnumerable? source)
    {
        if (source is null)
            return true;

        if (source is ICollection col)
            return col.Count == 0;

        var e = source.GetEnumerator();
        try
        {
            return !e.MoveNext();
        }
        finally
        {
            // Dispose if possible (pattern for non-generic IEnumerator)
            (e as IDisposable)?.Dispose();
        }
    }
}
