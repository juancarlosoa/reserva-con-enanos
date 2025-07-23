using RCE_Providers.Rooms.DTOs;

namespace RCE_Providers.Rooms.Services;

public interface IRoomService
{
    Task<RoomResponseDTO?> GetRoomByIdAsync(Guid roomId);
    Task<RoomResponseDTO> CreateRoomAsync(RoomRequestDTO dto);
    Task<bool> UpdateRoomAsync(Guid providerId, RoomRequestDTO dto);
    Task<bool> DeleteRoomAsync(Guid providerId);
}
