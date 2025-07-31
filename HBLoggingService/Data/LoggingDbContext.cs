namespace HBLoggingService.Data;

public class LoggingDbContext : DbContext
{
    public LoggingDbContext(DbContextOptions<LoggingDbContext> options)
        : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Qso>()
            .HasIndex(q => q.Call);
    }
    public DbSet<Qso> Qsos => Set<Qso>();
    public DbSet<QsoDetail> QsoDetails { get; set; }
    public DbSet<OperatorProfile> OperatorProfiles { get; set; }
    public DbSet<CallSign> CallSigns { get; set; }
    public DbSet<ServerLog> ServerLogs { get; set; }
}
