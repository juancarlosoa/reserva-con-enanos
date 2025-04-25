using AutoMapper;
using ReservaConEnanos.Reservations.DTOs;
using ReservaConEnanos.Reservations.Entities;
using ReservaConEnanos.Reservations.Repositories.Interfaces;
using ReservaConEnanos.Reservations.Services.Interfaces;

namespace ReservaConEnanos.Reservations.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _repository;
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ReservationResponseDto?> GetReservationByIdAsync(Guid id)
    {
        var reservation = await _repository.GetByIdAsync(id);

        return _mapper.Map<ReservationResponseDto>(reservation);
    }

    public async Task<ReservationResponseDto> CreateReservationAsync(ReservationRequestDto dto)
    {
        var reservation = _mapper.Map<Reservation>(dto);
        
        await _repository.AddAsync(reservation);
        await _repository.SaveChangesAsync();

        return _mapper.Map<ReservationResponseDto>(reservation);
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetDailyReservationsAsync(Guid escapeRoomId, DateTime date)
    {
        var reservations = await _repository.GetDailyReservationsAsync(escapeRoomId, date);

        return _mapper.Map<IEnumerable<ReservationResponseDto>>(reservations);
    }

    public async Task<IEnumerable<ReservationResponseDto>> GetMonthlyReservationsAsync(Guid escapeRoomId, DateTime date)
    {
        var reservations = await _repository.GetMonthlyReservationsAsync(escapeRoomId, date);
        return _mapper.Map<IEnumerable<ReservationResponseDto>>(reservations);
    }
}