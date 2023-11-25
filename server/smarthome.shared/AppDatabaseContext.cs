using Microsoft.EntityFrameworkCore;
using smarthome.shared.Models;

namespace smarthome.shared;

public class AppDatabaseContext : DbContext
{
    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceToken> DeviceTokens { get; set; }

    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure the database connection string
        // TODO: Fix this later, it should pull from settings by the running application
        //optionsBuilder.UseSqlServer("YourConnectionStringHere");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the Device model
        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Created).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.Updated).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.PublicKey).IsRequired();
        });

        // Configure the DeviceToken model
        modelBuilder.Entity<DeviceToken>(entity =>
        {
            entity.HasKey(e => e.DeviceId);
            entity.Property(e => e.Token).IsRequired();
            entity.Property(e => e.Expires).IsRequired();
        });
    }
}
