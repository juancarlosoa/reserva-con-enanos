using Microsoft.EntityFrameworkCore;
using ReservaConEnanos.Reservations.CoreData;
using ReservaConEnanos.Reservations.Entities;
using ReservaConEnanos.Reservations.Repositories.Interfaces;

namespace ReservaConEnanos.Reservations.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly ReservationsDbContext _context;
    
    public ReservationRepository(ReservationsDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation> AddAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);

        return reservation;
    }

    public async Task<Reservation?> GetByIdAsync(Guid id)
    {
        return await _context.Reservations.FindAsync(id);
    }

    public async Task<IEnumerable<Reservation>> GetDailyReservationsAsync(Guid escapeRoomId, DateTime date)
    {
        return await _context.Reservations
            .Where(x => x.EscapeRoomId == escapeRoomId &&
                        x.SessionDate == date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetMonthlyReservationsAsync(Guid escapeRoomId,DateTime date)
    {
        var endDate = date.AddMonths(1);

        return await _context.Reservations
            .Where(x => x.EscapeRoomId == escapeRoomId && 
                        x.SessionDate >= date && 
                        x.SessionDate < endDate)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
