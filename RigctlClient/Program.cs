using HbLibrary.RigControl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HamBlocks.Library.Models.Lookup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
namespace RigctlClient;

/// <summary>
/// This is a throw away console app to test the HamLibRigCtlClient and DxClusterClient classes.
/// It is not intended to be a complete application, but rather a simple test harness to verify
/// that the classes work as expected.  
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {

            string userName = string.Empty;
            string password = string.Empty;
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddUserSecrets<Program>();
                })
                .ConfigureServices((context, services) =>
                {
                    var configuration = context.Configuration;
                    if (configuration == null)
                        throw new InvalidOperationException("Configuration is not available.");

                    var userName = configuration["HamQth:Username"] ?? throw new InvalidOperationException("Username missing");
                    var password = configuration["HamQth:Password"] ?? throw new InvalidOperationException("Password missing");

                    services.AddMemoryCache();
                    services.AddHttpClient<HamQthLookupProvider>();
                    services.AddTransient<HamQthLookupProvider>(sp =>
                        new HamQthLookupProvider(
                            userName,
                            password,
                            sp.GetRequiredService<HttpClient>(),
                            "HamBlocksLib",
                            sp.GetRequiredService<IMemoryCache>()
                        )
                    );
                })
                .Build();

            var provider = host.Services.GetRequiredService<HamQthLookupProvider>();
            var result1 = await provider.LookupCallSignAsync("wa1gon");
            var result2 = await provider.LookupCallSignAsync("kb1etc");
            var result = await provider.LookupCallSignAsync("wa1gon");
            
            Console.WriteLine($"call: {result?.CallSign} State: {result?.State} Country: {result?.Country} Grid: {result?.Grid}");
            
    }

    private static async Task DxClusterTest()
    {
        var dxc = new HbLibrary.DxClusterClient("k4zr.no-ip.org", 7300);
        await dxc.ConnectAsync("wa1gon");
        Console.WriteLine("Logged successfully");
        await foreach (var line in dxc.GetSpotsAsync())
        {
            Console.WriteLine($"SPOT: {line.Spotter}: {line.Callsign} {line.Frequency}  {line.Timestamp}");
        }
        Console.WriteLine("End of method");
    }

    static private async Task RigTest()
    {
        HamLibRigCtlClient client = new HamLibRigCtlClient("localhost", 4532);
        await client.OpenAsync();  
        Console.WriteLine("Connected to rigctld");
        var freq = await client.GetFreqAsync();
        Console.WriteLine(freq);
        var caps = await client.GetCapabilitiesAsync();
    }
}
