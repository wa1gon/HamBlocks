using HbLibrary;

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
        modelBuilder.Entity<Qso>()
            .HasIndex(q => q.Band);
        modelBuilder.Entity<Qso>()
            .HasIndex(q => q.Dxcc);
        modelBuilder.Entity<QsoQslInfo>()
            .HasIndex(q => q.QslService);
        
        modelBuilder.Entity<LogConfig>()
            .HasMany(c => c.RigControls)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DxClusterConf>()
            .HasOne<LogConfig>()
            .WithMany()
            .HasForeignKey(c => c.HBConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<CallBookConf>()
            .HasOne<LogConfig>()
            .WithMany()
            .HasForeignKey(c => c.HBConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RigCtlConf>()
            .HasOne<LogConfig>()
            .WithMany()
            .HasForeignKey(r => r.HBConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);

    }
    public DbSet<Qso> Qsos => Set<Qso>();
    public DbSet<QsoDetail> QsoDetails { get; set; }
    public DbSet<QsoQslInfo> QsoQslInfos { get; set; }
    public DbSet<OperatorProfile> OperatorProfiles { get; set; }
    public DbSet<CallSign> CallSigns { get; set; }
    public DbSet<ServerLog> ServerLogs { get; set; }
    public DbSet<LogConfig> HBConfigurations { get; set; }
    public DbSet<CallBookConf> CallBookConfs { get; set; }
    public DbSet<RigCtlConf> RigCtlConfs { get; set; }

}
