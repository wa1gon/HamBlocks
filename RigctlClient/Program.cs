using HbLibrary.RigControl;

namespace RigctlClient;

class Program
{
    static async Task Main(string[] args)
    {
            // await RigTest();

            await DxClusterTest();
            
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
