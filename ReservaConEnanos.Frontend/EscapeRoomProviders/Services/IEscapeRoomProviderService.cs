using ReservaConEnanos.Frontend.EscapeRoomProviders.Entities;

namespace ReservaConEnanos.Frontend.EscapeRoomProviders.Services;

public interface IEscapeRoomProviderService
{
    Task<IEnumerable<EscapeRoomProvider>> GetAllProvidersAsync();
    Task<EscapeRoomProvider?> GetProviderByIdAsync(Guid id);
    Task<EscapeRoomProvider?> CreateProviderAsync(EscapeRoomProvider provider);
    Task<EscapeRoomProvider?> UpdateProviderAsync(EscapeRoomProvider provider);
    Task<bool> DeleteProviderAsync(Guid id);
}
