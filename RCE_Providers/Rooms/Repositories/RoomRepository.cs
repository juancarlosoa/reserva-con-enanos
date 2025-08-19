using RCE_Providers.CoreData;
using RCE_Providers.Rooms.Entities;
using Microsoft.EntityFrameworkCore;

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
    public void Delete(Room room)
    {
        if (room == null)
        {
            throw new ArgumentNullException(nameof(room), "Room cannot be null.");
        }

        _context.Rooms.Remove(room);
    }
    public async Task<Room?> GetByIdAsync(Guid id)
    {
        return await _context.Rooms.FindAsync(id);
    }
    public async Task<Room?> GetByProviderAndSlugAsync(Guid providerId, string roomSlug)
    {
        return await _context.Rooms.FirstOrDefaultAsync(r => r.ProviderId == providerId && r.Slug == roomSlug);
    }
    public void Update(Room room)
    {
        if (room == null)
        {
            throw new ArgumentNullException(nameof(room), "Room cannot be null.");
        }

        _context.Rooms.Update(room);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}