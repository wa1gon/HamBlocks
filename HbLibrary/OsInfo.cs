namespace HbLibrary;

public static class OsInfo
{
    /// <summary>
    ///     Return string for the current operating system type.
    ///     This method checks the operating system using the OperatingSystem class
    ///     and returns a string representation of the OS type.
    ///     Will be "Windows", "Linux", "macOS", "FreeBSD", "Android", "iOS", or "Unknown OS".
    /// </summary>
    public static string OsType
    {
        get
        {
            if (OperatingSystem.IsWindows()) return "Windows";

            if (OperatingSystem.IsLinux()) return "Linux";

            if (OperatingSystem.IsMacOS()) return "macOS";

            if (OperatingSystem.IsFreeBSD()) return "FreeBSD";

            if (OperatingSystem.IsAndroid()) return "Android";

            if (OperatingSystem.IsIOS()) return "iOS";

            return "Unknown OS";
        }
    }
}
