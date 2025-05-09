using AutoMapper;
using ReservaConEnanos.Providers.EscapeRoomProviders.DTOs;
using ReservaConEnanos.Providers.EscapeRoomProviders.Entities;

namespace ReservaConEnanos.Providers.EscapeRoomProviders.Mappings;

public class EscapeRoomProviderProfile : Profile
{
    public EscapeRoomProviderProfile() {
        CreateMap<EscapeRoomProviderCreateDTO, EscapeRoomProvider>();
        CreateMap<EscapeRoomProvider, EscapeRoomProviderResponseDTO>();
        CreateMap<EscapeRoomProviderUpdateDTO, EscapeRoomProvider>();
    }
}