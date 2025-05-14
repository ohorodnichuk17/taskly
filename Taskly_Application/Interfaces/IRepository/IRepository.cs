namespace Taskly_Application.Interfaces.IRepository;

public interface IRepository<T> where T : class
{ 
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
    Task SaveAsync(T entity);
}