namespace Logger.Core;

/// <summary>
/// Minimal service-locator wrapper (use sparingly).
/// Backed by the single Host service provider.
/// </summary>
public static class DIContainer
{
    private static IServiceProvider? _provider;

    /// <summary>
    /// Read-only access after initialization.
    /// </summary>
    public static IServiceProvider ServiceProvider =>
        _provider ?? throw new InvalidOperationException(
            "DIContainer not initialized. Call DIContainer.Initialize(...) once at startup.");

    /// <summary>
    /// Initialize exactly once with the Host's IServiceProvider.
    /// Safe if called multiple times with the same instance; will no-op.
    /// </summary>
    public static void Initialize(IServiceProvider provider)
    {
        if (provider is null) throw new ArgumentNullException(nameof(provider));
        // thread-safe set-once
        Interlocked.CompareExchange(ref _provider, provider, null);
    }

    /// <summary>
    /// Helper to resolve required service T.
    /// </summary>
    public static T Get<T>() where T : notnull =>
        ServiceProvider.GetRequiredService<T>();

    /// <summary>
    /// Create a scope when you need per-window/per-operation lifetimes.
    /// </summary>
    public static IServiceScope CreateScope() =>
        ServiceProvider.CreateScope();
}
