using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var filePath = @"C:\temp\mycallcheck.adi";
        var uploadUrl = "http://localhost:5000/upload";

        using var client = new HttpClient()
        {
            Timeout = TimeSpan.FromMinutes(15) // debug
        };
        using var form = new MultipartFormDataContent();
        using var fileStream = File.OpenRead(filePath);
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        // "File" must match the property name in your endpoint
        form.Add(fileContent, "File", Path.GetFileName(filePath));

        var response = await client.PostAsync(uploadUrl, form);
        var responseContent = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Status: {response.StatusCode}");
        Console.WriteLine("Response:");
        Console.WriteLine(responseContent);
    }
}
