using Microsoft.EntityFrameworkCore;
using ReservaConEnanos.Providers.CoreData;
using ReservaConEnanos.Providers.Rooms.Entities;

namespace ReservaConEnanos.Providers.Rooms.Repositories;

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
        await SaveChangesAsync();

        return room;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var room = await _context.Rooms.FindAsync(id);

        if (room == null) return false;

        _context.Rooms.Remove(room);
        await SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Room>> GetAllByProviderIdAsync(Guid providerId)
    {
        return await _context.Rooms.Where(r => r.ProviderId == providerId).ToListAsync();
    }

    public async Task<Room?> GetByIdAsync(Guid id)
    {
        return await _context.Rooms.FindAsync(id);
    }

    public async Task<Room> UpdateAsync(Room room)
    {
        _context.Rooms.Update(room);
        await SaveChangesAsync();

        return room;
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}