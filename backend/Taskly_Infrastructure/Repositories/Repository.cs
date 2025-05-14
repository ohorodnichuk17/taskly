using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly TasklyDbContext _context;
    internal DbSet<T> dbSet;

    public Repository(TasklyDbContext context)
    {
        _context = context;
        this.dbSet = _context.Set<T>();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            var entities = await dbSet.ToListAsync();
            return entities;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        try
        {
            var entity = await dbSet.FindAsync(id);
            if(entity == null)
            {
                throw new Exception($"{typeof(T).Name} not found");
            }
            return entity;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }

    public async Task<Guid> CreateAsync(T entity)
    {
        try
        {
            dbSet.Add(entity);
            await _context.SaveChangesAsync();
            var entityId = (Guid)typeof(T).GetProperty("Id")!.GetValue(entity)!;
            return entityId;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var entity = await dbSet.FindAsync(id);
            if (entity == null)
                throw new Exception($"{typeof(T).Name} not found"); 
            dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }

    public async Task SaveAsync(T entity)
    {
        try
        {
            dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }
}