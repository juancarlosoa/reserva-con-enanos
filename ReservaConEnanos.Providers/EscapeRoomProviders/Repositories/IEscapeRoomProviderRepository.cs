using ReservaConEnanos.Providers.EscapeRoomProviders.Entities;
using ReservaConEnanos.Providers.Rooms.Entities;

namespace ReservaConEnanos.Providers.EscapeRoomProviders.Repositories;

public interface IEscapeRoomProviderRepository
{
    Task<IEnumerable<EscapeRoomProvider>> GetAllAsync();
    Task<EscapeRoomProvider?> GetByIdAsync(Guid id);
    Task<EscapeRoomProvider> AddAsync(EscapeRoomProvider provider);
    Task<EscapeRoomProvider> UpdateAsync(EscapeRoomProvider provider);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<Room>> GetRoomsByProviderIdAsync(Guid id);
}
