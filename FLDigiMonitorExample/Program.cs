using System.Text;

internal class Program
{
    private static readonly HttpClient client = new();
    private static readonly string fldigiUrl = "http://localhost:7362/RPC2";
    private static string lastText = "";

    private static async Task Main(string[] args)
    {
        Console.WriteLine("JS8Call XML-RPC Client");
        Console.WriteLine($"Polling Fldigi at: {fldigiUrl}\n");

        while (true)
            try
            {
                var xmlRequest = @"<?xml version='1.0'?>
<methodCall>
  <methodName>rx.get_text</methodName>
</methodCall>";

                var content = new StringContent(xmlRequest, Encoding.UTF8, "text/xml");

                var response = await client.PostAsync(fldigiUrl, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                var decoded = ExtractValue(responseBody);
                if (!string.IsNullOrWhiteSpace(decoded) && decoded != lastText)
                {
                    Console.WriteLine($"[{DateTime.Now:T}] New Message:");
                    Console.WriteLine(decoded.Trim());
                    Console.WriteLine(new string('-', 40));
                    lastText = decoded;
                }

                await Task.Delay(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                await Task.Delay(5000);
            }
    }

    private static string ExtractValue(string xml)
    {
        const string tagStart = "<string>";
        const string tagEnd = "</string>";
        var start = xml.IndexOf(tagStart);
        var end = xml.IndexOf(tagEnd);

        if (start >= 0 && end > start) return xml.Substring(start + tagStart.Length, end - start - tagStart.Length);

        return "";
    }
}