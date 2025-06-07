using HBLoggingService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LoggingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/qsos", async (LoggingDbContext db) => await db.Qsos.ToListAsync());

app.MapPost("/qsos", async (LoggingDbContext db, Qso qso) =>
{
    db.Qsos.Add(qso);
    await db.SaveChangesAsync();
    return Results.Created($"/qsos/{qso.Id}", qso);
});

app.Run();
