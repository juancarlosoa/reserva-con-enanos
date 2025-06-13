using Microsoft.EntityFrameworkCore;
using RCE_Auth.CoreData;

namespace RCE_Auth.Shared.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    public AuthDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(AuthDbContext authDbContext)
    {
        _context = authDbContext;
        _dbSet = _context.Set<T>();
    }

    public async void AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetBdyIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}