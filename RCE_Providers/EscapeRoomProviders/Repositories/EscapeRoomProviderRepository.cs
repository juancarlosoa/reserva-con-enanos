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

        return provider;
    }

    public void Delete(EscapeRoomProvider provider)
    {
        if (provider == null)
        {
            throw new ArgumentNullException(nameof(provider), "Provider cannot be null.");
        }

        _context.EscapeRoomProviders.Remove(provider);
    }

    public async Task<IEnumerable<EscapeRoomProvider>> GetAllAsync()
    {
        return await _context.EscapeRoomProviders.ToListAsync();
    }

    public async Task<EscapeRoomProvider?> GetByIdAsync(Guid id)
    {
        return await _context.EscapeRoomProviders
             .Include(p => p.Rooms.OrderBy(r => r.CreatedAt).Take(5))
             .FirstOrDefaultAsync(p => p.Id == id);
    }

    public void Update(EscapeRoomProvider provider)
    {
        if (provider == null)
        {
            throw new ArgumentNullException(nameof(provider), "Provider cannot be null.");
        }

        _context.EscapeRoomProviders.Update(provider);
    }
    public async Task<IEnumerable<Room>> GetRoomsByProviderIdAsync(Guid id)
    {
        return await _context.Rooms.Where(r => r.ProviderId == id).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
