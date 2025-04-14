using Microsoft.EntityFrameworkCore;
using ReservaConEnanos.Models;

namespace ReservaConEnanos.CoreData;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<EscapeRoomProvider> EscapeRoomProviders => Set<EscapeRoomProvider>();
    public DbSet<EscapeRoom> EscapeRooms => Set<EscapeRoom>();
    public DbSet<AvailableSession> AvailableSessions => Set<AvailableSession>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AvailableSession>()
            .HasOne(s => s.Reservation)
            .WithOne(r => r.Session)
            .HasForeignKey<Reservation>(r => r.AvailableSessionId);
    }
}