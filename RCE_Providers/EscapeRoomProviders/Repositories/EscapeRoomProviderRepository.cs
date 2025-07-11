using System;
using Microsoft.EntityFrameworkCore;
using RCE_Providers.CoreData;
using RCE_Providers.EscapeRoomProviders.Entities;
using RCE_Providers.Rooms.Entities;

namespace RCE_Providers.EscapeRoomProviders.Repositories;

public class EscapeRoomProviderRepository : IEscapeRoomProviderRepository
{
    private readonly ProvidersDbContext _context;

    public EscapeRoomProviderRepository(ProvidersDbContext context)
    {
        _context = context;
    }

    public async Task<EscapeRoomProvider> AddAsync(EscapeRoomProvider provider)
    {
        await _context.EscapeRoomProviders.AddAsync(provider);
        await SaveChangesAsync();

        return provider;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var room = await _context.EscapeRoomProviders.FindAsync(id);

        if (room == null)
            return false;

        _context.EscapeRoomProviders.Remove(room);
        await SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<EscapeRoomProvider>> GetAllAsync()
    {
        return await _context.EscapeRoomProviders.ToListAsync();
    }

    public async Task<EscapeRoomProvider?> GetByIdAsync(Guid id)
    {
        return await _context.EscapeRoomProviders.FindAsync(id);
    }

    public async Task<EscapeRoomProvider> UpdateAsync(EscapeRoomProvider provider)
    {
        _context.EscapeRoomProviders.Update(provider);
        await SaveChangesAsync();

        return provider;
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Room>> GetRoomsByProviderIdAsync(Guid id)
    {
        return await _context.Rooms.Where(r => r.ProviderId == id).ToListAsync();
    }
}
