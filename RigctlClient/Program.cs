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
        var spot = dxc.GetSpotsAsync();
        spot.
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
