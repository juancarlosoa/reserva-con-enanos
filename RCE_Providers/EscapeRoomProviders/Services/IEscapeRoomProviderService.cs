using RCE_Providers.EscapeRoomProviders.DTOs;
using RCE_Providers.Rooms.DTOs;

namespace RCE_Providers.EscapeRoomProviders.Services;

public interface IEscapeRoomProviderService
{
    Task<IEnumerable<EscapeRoomProviderResponseDTO>> GetAllProviders();
    Task<EscapeRoomProviderResponseDTO?> GetProviderByIdAsync(Guid providerId);
    Task<EscapeRoomProviderResponseDTO> CreateProviderAsync(EscapeRoomProviderRequestDTO dto);
    Task<bool> UpdateProviderAsync(Guid providerId, EscapeRoomProviderRequestDTO dto);
    Task<bool> DeleteProviderAsync(Guid providerId);
    Task<IEnumerable<RoomResponseDTO>> GetRoomsByProviderIdAsync(Guid providerId);
}