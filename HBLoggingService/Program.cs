using FastEndpoints;
using FastEndpoints.Swagger;
using HBLoggingService.Data;
using HBLoggingService.Options;
using HBLoggingService.Options;
var builder = WebApplication.CreateBuilder(args);
// debugging
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(15);
    serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(15);
    // You can add more Kestrel options here as needed
});
var dbProvider = builder.Configuration["DatabaseProvider"]?.ToLowerInvariant();

builder.Services.AddOptions<StationConfig>()
    .BindConfiguration("StationConfig");

builder.Services.AddDbContext<LoggingDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

    switch (dbProvider)
    {
        case "postgresql":
            options.UseNpgsql(connStr);
            break;

        // case "sqlite":
        //     options.UseSqlite(connStr);
        //     break;
        //
        // case "sqlserver":
        //     options.UseSqlServer(connStr);
        //     break;

        default:
            throw new InvalidOperationException($"Unsupported database provider: {dbProvider}");
    }
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocument();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LoggingDbContext>();
    db.Database.EnsureCreated();
}
app.UseSwagger();
app.UseSwaggerUI();
// app.UseAuthorization();
app.UseFastEndpoints().UseSwaggerGen();
app.UseOpenApi();
app.UseSwaggerUi(x => x.ConfigureDefaults());
app.MapGet("/qsos", async (LoggingDbContext db) => 
    await db.Qsos.Include(q => q.Details).ToListAsync());

app.MapPost("/qsos", async (LoggingDbContext db, Qso qso) =>
{
    db.Qsos.Add(qso);
    await db.SaveChangesAsync();
    return Results.Created($"/qsos/{qso.Id}", qso);
});
app.Urls.Add("http://localhost:5000");
app.Run();
