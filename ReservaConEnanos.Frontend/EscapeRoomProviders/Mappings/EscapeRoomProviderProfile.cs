using AutoMapper;
using ReservaConEnanos.Frontend.EscapeRoomProviders.DTOs;
using ReservaConEnanos.Frontend.EscapeRoomProviders.Entities;

namespace ReservaConEnanos.Frontend.EscapeRoomProviders.Mappings;

public class EscapeRoomProviderProfile : Profile
{
    public EscapeRoomProviderProfile() {
        CreateMap<EscapeRoomProviderResponseDTO, EscapeRoomProvider>();
        CreateMap<EscapeRoomProvider, EscapeRoomProviderCreateDTO>();
        CreateMap<EscapeRoomProvider, EscapeRoomProviderUpdateDTO>();
    }
}
