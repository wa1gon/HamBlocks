namespace HBLoggingService.Data;

public class LoggingDbContext : DbContext
{
    public LoggingDbContext(DbContextOptions<LoggingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Qso> Qsos => Set<Qso>();
}
