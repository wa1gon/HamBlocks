using HbLibrary.RigControl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HamBlocks.Library.Models.Lookup;
using HamBlocks.Library.Models.Lookup.Qrz;
using HbLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using HbLibrary.FileIO;

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

                    var hamqthUser = configuration["HamQth:Username"] ?? throw new InvalidOperationException("HamQTH Username missing");
                    var hamqthPassword = configuration["HamQth:Password"] ?? throw new InvalidOperationException("HamQTH Password missing");
                    var qrzUserName = configuration["QRZ:Username"] ?? throw new InvalidOperationException("QRZ Username missing");
                    var qrzPassword = configuration["QRZ:Password"] ?? throw new InvalidOperationException("QRZ Password missing");
                    services.AddMemoryCache();
                    services.AddHttpClient<HamQthLookupProvider>();
                    services.AddTransient<HamQthLookupProvider>(sp =>
                        new HamQthLookupProvider(
                            hamqthUser,
                            hamqthPassword,
                            sp.GetRequiredService<HttpClient>(),
                            "HamBlocksLib",
                            sp.GetRequiredService<IMemoryCache>()
                        )
                    );
                    services.AddTransient<QrzLookupProvider>(sp =>
                        new QrzLookupProvider(
                            qrzUserName,
                            password: qrzPassword,
                            sp.GetRequiredService<HttpClient>(),
                            "HamBlocksLib",
                            sp.GetRequiredService<IMemoryCache>()
                        )
                    );
                })
                .Build();
            
            var dxccList = DxccJsonReader.LoadDxccFromJson("C:/temp/dxcc.json");
            var dxccEntity = DxccCallInfo.FindMatchingPrefix("wa1gon", dxccList);
            if (dxccEntity != null)
            {
                Console.WriteLine($"Found DXCC: {dxccEntity.Name} ({dxccEntity.CountryCode})");
            }
            dxccEntity = DxccCallInfo.FindMatchingPrefix("kh6ff", dxccList);
            if (dxccEntity != null)
            {
                Console.WriteLine($"Found DXCC: {dxccEntity.Name} ({dxccEntity.CountryCode})");
            }            

            // if (Dxcc.FindMatchingPrefix("wa1gon", dxccList) is DxccEntity dxcc)
            // {
            //     Console.WriteLine($"Found DXCC: {dxcc.Name} ({dxcc.CountryCode})");
            // }
            // else
            // {
            //     Console.WriteLine("No matching DXCC found for call sign 'wa1gon'.");
            // });
            
            if (false)
            {
                var HamQthprovider = host.Services.GetRequiredService<HamQthLookupProvider>();
                var result1 = await HamQthprovider.LookupCallSignAsync("wa1gon");
                var result2 = await HamQthprovider.LookupCallSignAsync("kb1etc");
                var result = await HamQthprovider.LookupCallSignAsync("wa1gon");

                Console.WriteLine(
                    $"call: {result?.CallSign} State: {result?.State} Country: {result?.Country} Grid: {result?.Grid}");

                var dxcc = await HamQthprovider.LookupDxccByCallAsync("wa1gon");
                Console.WriteLine($"DXCC: {dxcc?.CallSign} Name: {dxcc?.Name} Continent: {dxcc?.Continent} ");

                // QRZ lookup test
                var Qrzprovider = host.Services.GetRequiredService<QrzLookupProvider>();
                var qrzrc1 = await Qrzprovider.LookupCallSignAsync("wa1gon");
                var qrzrc2 = await Qrzprovider.LookupCallSignAsync("kb1etc");
                var qrzrc3 = await Qrzprovider.LookupCallSignAsync("wa1gon");

                Console.WriteLine(
                    $"call: {qrzrc1?.CallSign} State: {qrzrc1?.State} Country: {qrzrc1?.Country} Grid: {qrzrc1?.Grid}");

                var dxccrc = await HamQthprovider.LookupDxccByCallAsync("wa1gon");
                Console.WriteLine($"DXCC: {dxccrc?.CallSign} Name: {dxccrc?.Name} Continent: {dxccrc?.Continent} ");
            }
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
