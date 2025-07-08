using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static readonly HttpClient client = new HttpClient();
    static string fldigiUrl = "http://localhost:7362/RPC2";
    static string lastText = "";

    static async Task Main(string[] args)
    {
        Console.WriteLine("JS8Call XML-RPC Client");
        Console.WriteLine($"Polling Fldigi at: {fldigiUrl}\n");

        while (true)
        {
            try
            {
                string xmlRequest = @"<?xml version='1.0'?>
<methodCall>
  <methodName>rx.get_text</methodName>
</methodCall>";

                var content = new StringContent(xmlRequest, Encoding.UTF8, "text/xml");

                var response = await client.PostAsync(fldigiUrl, content);
                string responseBody = await response.Content.ReadAsStringAsync();

                string decoded = ExtractValue(responseBody);
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
    }

    static string ExtractValue(string xml)
    {
        const string tagStart = "<string>";
        const string tagEnd = "</string>";
        int start = xml.IndexOf(tagStart);
        int end = xml.IndexOf(tagEnd);

        if (start >= 0 && end > start)
        {
            return xml.Substring(start + tagStart.Length, end - start - tagStart.Length);
        }

        return "";
    }
}
