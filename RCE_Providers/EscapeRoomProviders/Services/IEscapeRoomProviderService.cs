using RCE_Providers.EscapeRoomProviders.DTOs;
using RCE_Providers.Rooms.DTOs;

namespace RCE_Providers.EscapeRoomProviders.Services;

public interface IEscapeRoomProviderService
{
    Task<IEnumerable<EscapeRoomProviderResponseDTO>> GetAllProviders();
    Task<EscapeRoomProviderResponseDTO?> GetProviderByIdAsync(Guid providerId);
    Task<EscapeRoomProviderResponseDTO> CreateProviderAsync(EscapeRoomProviderCreateDTO dto);
    Task<EscapeRoomProviderResponseDTO> UpdateProviderAsync(EscapeRoomProviderUpdateDTO dto);
    Task<bool> DeleteProvider(Guid providerId);
    Task<IEnumerable<RoomResponseDTO>> GetRoomsByProviderIdAsync(Guid providerId);
}