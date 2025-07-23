using AutoMapper;
using RCE_Providers.EscapeRoomProviders.DTOs;
using RCE_Providers.EscapeRoomProviders.Entities;

namespace RCE_Providers.EscapeRoomProviders.Mappings;

public class EscapeRoomProviderProfile : Profile
{
    public EscapeRoomProviderProfile()
    {
        CreateMap<EscapeRoomProviderRequestDTO, EscapeRoomProvider>();
        CreateMap<EscapeRoomProvider, EscapeRoomProviderResponseDTO>();
    }
}