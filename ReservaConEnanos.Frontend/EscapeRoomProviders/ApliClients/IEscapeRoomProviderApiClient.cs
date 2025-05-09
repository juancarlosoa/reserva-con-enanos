using ReservaConEnanos.Frontend.EscapeRoomProviders.DTOs;

namespace ReservaConEnanos.Frontend.EscapeRoomProviders.ApiClients;

public interface IEscapeRoomProviderApiClient
{
    Task<IEnumerable<EscapeRoomProviderResponseDTO>?> GetAllProvidersAsync();
    Task<EscapeRoomProviderResponseDTO?> GetProviderByIdAsync(Guid id);
    Task<EscapeRoomProviderResponseDTO?> CreateProviderAsync(EscapeRoomProviderCreateDTO dto);
    Task<EscapeRoomProviderResponseDTO?> UpdateProviderAsync(EscapeRoomProviderUpdateDTO dto);
    Task<bool> DeleteProviderAsync(Guid id);
}