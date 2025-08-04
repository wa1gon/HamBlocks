
namespace HBLibrary.Test;

[TestClass]
public class HamLibRigCtlClientTests
{
    private const string Host = "127.0.0.1";
    private const int Port = 4532; // Default rigctld port

    [TestMethod]
    public async Task Open_SetFreq_SetMode_AssertMode_Close_Works()
    {
        var client = new HamLibRigCtlClient(Host, Port);

        await client.OpenAsync();

        long testFreq = 14074000;
        await client.SetFreqAsync(testFreq);
        Assert.AreEqual(testFreq, client.Freq);

        string testMode = "USB";
        await client.SetModeAsync(testMode);

        // Send mode query, ensure no exception
        await client.SendCommandAsync("m\n");

        client.Close();
    }

    [TestMethod]
    public async Task OpenAsync_Throws_OnInvalidPort()
    {
        var client = new HamLibRigCtlClient(Host, 65530); // Unlikely to be used

        await Assert.ThrowsExceptionAsync<IOException>(async () =>
        {
            await client.OpenAsync();
        });
    }
}
