using RCE_Providers.Rooms.DTOs;

namespace RCE_Providers.Rooms.Services;

public interface IRoomService
{
    Task<RoomResponseDTO?> GetRoomBySlugsAsync(string providerSlug, string roomSlug);
    Task<RoomResponseDTO?> CreateRoomByProviderSlugAsync(string providerSlug, RoomRequestDTO dto);
    Task<bool> UpdateRoomBySlugsAsync(string providerSlug, string roomSlug, RoomRequestDTO dto);
    Task<bool> DeleteRoomBySlugsAsync(string providerSlug, string roomSlug);
    Task<IEnumerable<RoomResponseDTO>> GetRoomsByProviderSlugAsync(string providerSlug);
}
