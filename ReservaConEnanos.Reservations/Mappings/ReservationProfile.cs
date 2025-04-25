using AutoMapper;
using ReservaConEnanos.Reservations.DTOs;
using ReservaConEnanos.Reservations.Entities;

namespace ReservaConEnanos.Reservations.Mappings;

public class ReservationProfile : Profile
{
    public ReservationProfile() 
    {
        CreateMap<ReservationRequestDto, Reservation>();
        CreateMap<Reservation, ReservationResponseDto>();
    }
}
