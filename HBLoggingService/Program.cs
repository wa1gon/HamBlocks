using HBLoggingService.Data;

var builder = WebApplication.CreateBuilder(args);

var dbProvider = builder.Configuration["DatabaseProvider"]?.ToLowerInvariant();

builder.Services.AddDbContext<LoggingDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connStr);
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
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LoggingDbContext>();
    db.Database.EnsureCreated();
}
app.UseSwagger();
app.UseSwaggerUI();

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
