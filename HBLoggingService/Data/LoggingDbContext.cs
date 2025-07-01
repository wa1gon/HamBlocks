namespace HBLoggingService.Data;

public class LoggingDbContext : DbContext
{
    public LoggingDbContext(DbContextOptions<LoggingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Qso> Qsos => Set<Qso>();
    public DbSet<QsoDetail> QsoDetails { get; set; }
    public DbSet<OperatorProfile> OperatorProfiles { get; set; }
    public DbSet<CallSign> CallSigns { get; set; }
}
