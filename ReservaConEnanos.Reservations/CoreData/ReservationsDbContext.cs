using Microsoft.EntityFrameworkCore;
using ReservaConEnanos.Reservations.Entities;

namespace ReservaConEnanos.Reservations.CoreData;

public class ReservationsDbContext: DbContext
{
    public ReservationsDbContext(DbContextOptions<ReservationsDbContext> options) : base(options) {}
    public DbSet<Reservation> Reservations => Set<Reservation>();
}