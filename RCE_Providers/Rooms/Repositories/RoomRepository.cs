using RCE_Providers.CoreData;
using RCE_Providers.Rooms.Entities;

namespace RCE_Providers.Rooms.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ProvidersDbContext _context;

    public RoomRepository(ProvidersDbContext context)
    {
        _context = context;
    }

    public async Task<Room> AddAsync(Room room)
    {
        await _context.Rooms.AddAsync(room);
        return room;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null) return false;

        _context.Rooms.Remove(room);

        return true;
    }

    public async Task<Room?> GetByIdAsync(Guid id)
    {
        return await _context.Rooms.FindAsync(id);
    }

    public Room UpdateAsync(Room room)
    {
        _context.Rooms.Update(room);
        return room;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}