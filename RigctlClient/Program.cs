using HbLibrary.RigControl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HamBlocks.Library.Models.Lookup;
using Microsoft.Extensions.Configuration;

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
            // await RigTest();

            // await DxClusterTest();
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
                    userName = configuration["HamQth:Username"];
                    password = configuration["HamQth:Password"];

                    services.AddHttpClient<HamQthLookupProvider>();
                    services.AddTransient(sp =>
                        new HamQthLookupProvider(userName, password, sp.GetRequiredService<HttpClient>()));
                })
                .Build();

            var provider = host.Services.GetRequiredService<HamQthLookupProvider>();
            var result = await provider.LookupAsync("wa1gon");
            Console.WriteLine(result?.CallSign);
            
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
