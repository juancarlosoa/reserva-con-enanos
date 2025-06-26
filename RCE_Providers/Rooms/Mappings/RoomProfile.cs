using AutoMapper;
using RCE_Providers.Rooms.DTOs;
using RCE_Providers.Rooms.Entities;

namespace RCE_Providers.Rooms.Mappings;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<RoomRequestDTO, Room>();
        CreateMap<Room, RoomResponseDTO>();
    }
}
