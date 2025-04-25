using ReservaConEnanos.Reservations.Entities;

namespace ReservaConEnanos.Reservations.Repositories.Interfaces;

public interface IReservationRepository
{
    Task<Reservation> AddAsync(Reservation reservation);
    Task<Reservation?> GetByIdAsync(Guid id);
    Task<IEnumerable<Reservation>> GetMonthlyReservationsAsync(Guid escapeRoomId, DateTime date);
    Task<IEnumerable<Reservation>> GetDailyReservationsAsync(Guid escapeRoomId, DateTime date);
    Task SaveChangesAsync();
}