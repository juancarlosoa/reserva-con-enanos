using AutoMapper;
using ReservaConEnanos.Providers.Rooms.DTOs;
using ReservaConEnanos.Providers.Rooms.Entities;

namespace ReservaConEnanos.Providers.Rooms.Mappings;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<RoomRequestDTO, Room>();
        CreateMap<Room, RoomResponseDTO>();
    }
}
