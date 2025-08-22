using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Spectre.Console;

class Program
{
    private const string DefaultUploadUrl = "http://127.0.0.1:7300"; // Default for JS8Call API
    private static readonly HttpClient _client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
    private static string _filePath = @"C:\temp\mycallcheck.adi";
    private static string _uploadUrl = DefaultUploadUrl;

    static async Task Main(string[] args)
    {
        // var filePath = @"C:\temp\mycallcheck.adi";
        // var uploadUrl = "http://localhost:7300/api/uploadadif";

        //             if (Uri.TryCreate(input, UriKind.Absolute, out var uriResult) &&
        //                 (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
        //             {
        //                 return ValidationResult.Success();
        //             }
        //             return ValidationResult.Error("Please enter a valid absolute URL (e.g., http://127.0.0.1:24400).");
        //         })
        // );

        // Select ADIF file
        // _filePath = SelectFileInteractively();
        AnsiConsole.MarkupLine($"You selected: [green]{_filePath}[/]");
        // }
        // Upload the file
        await UploadAdif();
        // }
        // catch (Exception ex)
        // {
        //     AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
        // }
    }

    public static string SelectFileInteractively()
    {
        var goUpEntry = "[[Go up]]";
        string currentPath = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter starting directory:")
                .Validate(path => Directory.Exists(path)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Directory does not exist[/]"))
        );

        while (true)
        {
            var entries = Directory.GetFileSystemEntries(currentPath)
                .Where(e => Directory.Exists(e) || Path.GetExtension(e).Equals(".adi", StringComparison.OrdinalIgnoreCase))
                .Concat(new[] { goUpEntry })
                .ToArray();

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"Contents of [green]{currentPath}[/]:")
                    .AddChoices(entries));

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
                // Confirm file selection
                if (AnsiConsole.Confirm($"Upload {choice}?"))
                    return choice;
            }
        }
    }

    private static async Task UploadAdif()
    {
        if (!File.Exists(_filePath))
        {
            AnsiConsole.MarkupLine("[red]Error: ADIF file does not exist.[/]");
            return;
        }

        try
        {
            using var form = new MultipartFormDataContent();
            using var fileStream = File.OpenRead(_filePath);
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            form.Add(fileContent, "File", Path.GetFileName(_filePath));

            var response = await _client.PostAsync(_uploadUrl, form);
            response.EnsureSuccessStatusCode(); // Throws if not successful
            var responseContent = await response.Content.ReadAsStringAsync();

            AnsiConsole.MarkupLine($"[green]Status: {response.StatusCode}[/]");
            AnsiConsole.MarkupLine("Response:");
            AnsiConsole.MarkupLine(responseContent);
        }
        catch (HttpRequestException ex)
        {
            AnsiConsole.MarkupLine($"[red]HTTP Error: {ex.Message}[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Upload Error: {ex.Message}[/]");
        }
    }
}
