namespace HBWebLogger;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var apiUrl = builder.Configuration["LogApiServer:Url"] ?? "http://localhost:7300/api/";
        
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddMudServices();
        builder.Services.AddHttpClient<HamQthLookupProvider>();
        builder.Services.AddScoped<HbConfClientApiService>();
        builder.Services.AddSingleton<DxccInfoClientService>();
        builder.Services.AddHttpClient("SharedClient", c =>
        {
            c.BaseAddress = new Uri("http://localhost:7300/api/"); // trailing slash
        });

        builder.Services.AddScoped<HbConfClientApiService>();
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
