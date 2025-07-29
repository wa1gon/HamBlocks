using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Spectre.Console;

class Program
{
    static string filePath = @"C:\temp\mycallcheck.adi";
    static string uploadUrl = "http://localhost:5000/uploadadif";
    static async Task Main(string[] args)
    {
        // AnsiConsole.MarkupLine("[red bold]Hello, World![/]");
        // AnsiConsole.MarkupLine("Hello, World!");
        // AnsiConsole.MarkupLine("[slowblink]Hello, World![/]");
        // Console.ReadKey();
        // AnsiConsole.Clear();

        uploadUrl = AnsiConsole.Prompt(
            new TextPrompt<string>($"QSO Upload URL [[default is {uploadUrl}]]:")
                .DefaultValue(uploadUrl)
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input))
                        return ValidationResult.Success();

                    if (Uri.TryCreate(input, UriKind.Absolute, out var uriResult) &&
                        (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                    {
                        return ValidationResult.Success();
                    }

                    return ValidationResult.Error("Please enter a valid URL (e.g., http://127.0.0.1:24400).");
                })
        );
        filePath = SelectFileInteractively();
        AnsiConsole.MarkupLine($"You selected: [green]{filePath}[/]");
        //await UploadAdif();
    }

    public static string SelectFileInteractively()
    {
        string currentPath = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter starting directory:")
                .Validate(path => Directory.Exists(path)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Directory does not exist[/]"))
        );

        while (true)
        {
            var entries = Directory.GetFileSystemEntries(currentPath);
            var choices = entries.Concat(new[] { "[Go up]" }).ToArray();
            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"Contents of [green]{currentPath}[/]:")
                    .AddChoices(choices)
                    .UseMarkup(false)
            );

            if (choice == "[Go up]")
            {
                currentPath = Directory.GetParent(currentPath)?.FullName ?? currentPath;
                continue;
            }

            if (Directory.Exists(choice))
            {
                currentPath = choice;
            }
            else
            {
                return choice;
            }
        }
    }
    private static async Task UploadAdif()
    {
        // var filePath = @"C:\temp\mycallcheck.adi";
        // var uploadUrl = "http://localhost:5000/uploadadif";

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
