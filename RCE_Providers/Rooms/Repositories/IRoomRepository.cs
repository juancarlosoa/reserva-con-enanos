using RCE_Providers.Rooms.Entities;

namespace RCE_Providers.Rooms.Repositories;

public interface IRoomRepository
{
        Task<Room?> GetByIdAsync(Guid id);
        Task<Room> AddAsync(Room room);
        Room UpdateAsync(Room room);
        Task<bool> DeleteAsync(Guid id);
        Task SaveChangesAsync();
}
