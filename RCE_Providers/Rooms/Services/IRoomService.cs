using RCE_Providers.Rooms.DTOs;

namespace RCE_Providers.Rooms.Services;

public interface IRoomService
{
    Task<RoomResponseDTO?> GetRoomByIdAsync(Guid roomId);
    Task<RoomResponseDTO> CreateRoomAsync(RoomRequestDTO dto);
    Task<RoomResponseDTO> UpdateRoomAsync(RoomRequestDTO dto);
    Task<bool> DeleteRoom(Guid roomId);
}
