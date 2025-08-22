namespace HBWebLogger;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var apiUrl = builder.Configuration["LogApiServer:Url"] ?? "http://localhost:7300/api";
        // Add services to the container.
        
        builder.Services.AddScoped<HbConfClientApiService>();
        builder.Services.AddHttpClient<HbConfClientApiService>(client =>
        {
            client.BaseAddress = new Uri(apiUrl);
        });

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddMudServices();
        builder.Services.AddHttpClient<HamQthLookupProvider>();
        builder.Services.AddSingleton<DxccInfoClientService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
   

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        // app.UseAuthorization();
        app.MapControllers();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}
