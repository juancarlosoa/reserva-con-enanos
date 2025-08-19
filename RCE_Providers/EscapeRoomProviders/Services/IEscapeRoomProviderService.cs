using RCE_Providers.EscapeRoomProviders.DTOs;

namespace RCE_Providers.EscapeRoomProviders.Services;

public interface IEscapeRoomProviderService
{
    Task<IEnumerable<EscapeRoomProviderResponseDTO>> GetAllProviders();
    Task<EscapeRoomProviderResponseDTO?> GetProviderBySlugAsync(string slug);
    Task<EscapeRoomProviderResponseDTO> CreateProviderAsync(EscapeRoomProviderRequestDTO dto);
    Task<bool> UpdateProviderBySlugAsync(string providerSlug, EscapeRoomProviderRequestDTO dto);
    Task<bool> DeleteProviderBySlugAsync(string providerSlug);
}