using ReservaConEnanos.Reservations.DTOs;

namespace ReservaConEnanos.Reservations.Services.Interfaces;

public interface IReservationService
{
    Task<ReservationResponseDto?> GetReservationByIdAsync(Guid id);
    Task<ReservationResponseDto> CreateReservationAsync(ReservationRequestDto dto);
    Task<IEnumerable<ReservationResponseDto>> GetDailyReservationsAsync(Guid escapeRoomId, DateTime date);
    Task<IEnumerable<ReservationResponseDto>> GetMonthlyReservationsAsync(Guid escapeRoomId, DateTime date);
}