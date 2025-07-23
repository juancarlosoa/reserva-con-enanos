using RCE_Providers.Rooms.Entities;

namespace RCE_Providers.Rooms.Repositories;

public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(Guid id);
    Task<Room> AddAsync(Room room);
    void Update(Room room);
    void Delete(Room room);
    Task SaveChangesAsync();
}