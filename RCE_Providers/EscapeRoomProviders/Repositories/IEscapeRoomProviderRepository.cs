using RCE_Providers.EscapeRoomProviders.Entities;
using RCE_Providers.Rooms.Entities;

namespace RCE_Providers.EscapeRoomProviders.Repositories;

public interface IEscapeRoomProviderRepository
{
    Task<IEnumerable<EscapeRoomProvider>> GetAllAsync();
    Task<EscapeRoomProvider?> GetByIdAsync(Guid id);
    Task<EscapeRoomProvider?> GetBySlugAsync(string slug);
    Task<EscapeRoomProvider> AddAsync(EscapeRoomProvider provider);
    void Update(EscapeRoomProvider provider);
    void Delete(EscapeRoomProvider provider);
    Task<IEnumerable<Room>> GetRoomsByProviderIdAsync(Guid id);
    Task SaveChangesAsync();
}
