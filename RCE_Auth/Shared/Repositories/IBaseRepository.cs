namespace RCE_Auth.Shared.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetBdyIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    void AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}