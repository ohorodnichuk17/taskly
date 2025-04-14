using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class ToDoTableRepository(TasklyDbContext tasklyDbContext) : Repository<ToDoTableEntity>(tasklyDbContext), IToDoTableRepository
{
    public async Task<ToDoTableEntity?> GetToDoTableIncludeById(Guid TableId)
    {
        var table = await dbSet.Include(t => t.ToDoItems)
                .ThenInclude(ti => ti.Members)
                .ThenInclude(m => m.Avatar)
                .Include(t => t.ToDoItems)
                .ThenInclude(ti => ti.TimeRange)
                .FirstOrDefaultAsync(t => t.Id == TableId);

        return table;
    }

    public async Task<ToDoTableEntity> CreateNewToDoTableAsync(string name)
    {
        var newTable = new ToDoTableEntity() { Id = Guid.NewGuid(), Name = name };
        if (newTable.Members == null)
            newTable.Members = new List<UserEntity>();

        await CreateAsync(newTable);

        return newTable;
    }

    public async Task<UserEntity> AddNewUserToToDoTableAsync(ToDoTableEntity table, UserEntity user)
    {
        if (table.Members == null)
            table.Members = new List<UserEntity>();

        table.Members.Add(user);

        if (user.ToDoTables == null)
            user.ToDoTables = new List<ToDoTableEntity>();

        user.ToDoTables.Add(table);

        await SaveAsync(table); 

        return user;
    }

    public async Task<ICollection<ToDoTableEntity>> GetToDoTablesByUserIdAsync(Guid userId)
    {
        var tablesId = await tasklyDbContext.Set<Dictionary<string, object>>("UserTable")
            .Where(ut => (Guid)ut["UserId"] == userId)
            .Select(ut => (Guid)ut["ToDoTableId"])
            .ToListAsync();

        var tables = await tasklyDbContext.ToDoTables
            .Where(t => tablesId.Any(t_ => t.Id == t_))
            .Include(t => t.Members)
            .ToListAsync();

        return tables;
    }

    public async Task<bool> DeleteToDoTableAsync(Guid tableId)
    {
        var table = await tasklyDbContext.ToDoTables.FirstOrDefaultAsync(t => t.Id == tableId);
        if (table == null)
            return false;
        tasklyDbContext.ToDoTables.Remove(table);
        await tasklyDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<ToDoTableEntity> EditToDoTableAsync(Guid tableId, string name)
    {
        var table = await tasklyDbContext.ToDoTables.FirstOrDefaultAsync(t => t.Id == tableId);
        if (table == null)
            throw new Exception("Table not found");

        table.Name = name;
        tasklyDbContext.ToDoTables.Update(table); 
        await tasklyDbContext.SaveChangesAsync(); 
        return table;
    }
    
    private async Task<(ToDoTableEntity table, UserEntity user)> GetTableAndUserAsync(Guid tableId, Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("TableId and UserId must not be empty");
        var table = await GetToDoTableIncludeById(tableId);
        var user = await tasklyDbContext.Users.FindAsync(userId);
        if (user == null)
            throw new KeyNotFoundException("User not found");
        return (table, user);
    }
    
    private async Task<ToDoTableEntity> GetTableByConditionAsync(Expression<Func<ToDoTableEntity, bool>> condition)
    {
        var table = await tasklyDbContext.ToDoTables
            .Include(b => b.Members)
            .Include(b => b.ToDoItems)
            .FirstOrDefaultAsync(condition);

        if (table == null)
            throw new KeyNotFoundException("Table not found");

        return table;
    }
}
