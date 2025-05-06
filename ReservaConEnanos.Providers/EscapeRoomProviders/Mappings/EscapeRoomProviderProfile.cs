using AutoMapper;
using ReservaConEnanos.Providers.EscapeRoomProviders.DTOs;
using ReservaConEnanos.Providers.EscapeRoomProviders.Entities;

namespace ReservaConEnanos.Providers.EscapeRoomProviders.Mappings;

public class EscapeRoomProviderProfile : Profile
{
    public EscapeRoomProviderProfile() {
        CreateMap<EscapeRoomProviderRequestDTO, EscapeRoomProvider>();
        CreateMap<EscapeRoomProvider, EscapeRoomProviderResponseDTO>();
    }
}