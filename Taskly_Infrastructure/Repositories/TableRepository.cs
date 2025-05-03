using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Taskly_Application.DTO;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class TableRepository(TasklyDbContext tasklyDbContext) : Repository<TableEntity>(tasklyDbContext), ITableRepository
{
    public async Task<TableEntity?> GetTableIncludeByIdAsync(Guid tableId)
    {
        var table = await dbSet
            .Include(t => t.Members) 
            .ThenInclude(m => m.Avatar) 
            .Include(t => t.ToDoItems)
            .ThenInclude(ti => ti.Members)
            .ThenInclude(m => m.Avatar)
            .Include(t => t.ToDoItems)
            .ThenInclude(ti => ti.TimeRange)
            .FirstOrDefaultAsync(t => t.Id == tableId);

        return table;
    }

    public async Task<TableEntity> CreateNewTableAsync(string name)
    {
        var newTable = new TableEntity() { Id = Guid.NewGuid(), Name = name };
        if (newTable.Members == null)
            newTable.Members = new List<UserEntity>();

        await CreateAsync(newTable);

        return newTable;
    }

    public async Task<UserEntity> AddNewUserToTableAsync(TableEntity table, UserEntity user)
    {
        if (table.Members == null)
            table.Members = new List<UserEntity>();

        table.Members.Add(user);

        if (user.ToDoTables == null)
            user.ToDoTables = new List<TableEntity>();

        user.ToDoTables.Add(table);

        await SaveAsync(table); 

        return user;
    }

    public async Task<ICollection<TableEntity>> GetTablesByUserIdAsync(Guid userId)
    {
        var tablesId = await tasklyDbContext.Set<Dictionary<string, object>>("UserTable")
            .Where(ut => (Guid)ut["UserId"] == userId)
            .Select(ut => (Guid)ut["TableId"])
            .ToListAsync();

        var tables = await tasklyDbContext.ToDoTables
            .Where(t => tablesId.Any(t_ => t.Id == t_))
            .Include(t => t.Members)
            .ToListAsync();

        return tables;
    }

    public async Task<bool> DeleteTableAsync(Guid tableId)
    {
        var table = await tasklyDbContext.ToDoTables.FirstOrDefaultAsync(t => t.Id == tableId);
        if (table == null)
            return false;
        tasklyDbContext.ToDoTables.Remove(table);
        await tasklyDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<TableEntity> EditTableAsync(Guid tableId, string name)
    {
        var table = await tasklyDbContext.ToDoTables.FirstOrDefaultAsync(t => t.Id == tableId);
        if (table == null)
            throw new Exception("Table not found");

        table.Name = name;
        tasklyDbContext.ToDoTables.Update(table); 
        await tasklyDbContext.SaveChangesAsync(); 
        return table;
    }

    public async Task AddMemberToTableAsync(Guid tableId, Guid userId)
    {
        var (table, user) = await GetTableAndUserAsync(tableId, userId);
        ValidateTableMembers(table);
        if (table.Members.Any(m => m.Id == userId))
            throw new ArgumentException("User already exists in the table");
        table.Members ??= new List<UserEntity>();
        table.Members.Add(user);
        user.ToDoTables.Add(table);
        var tableUserRelations = new Dictionary<string, object>();
        tableUserRelations.Add("TableId", table.Id);
        tableUserRelations.Add("UserId", user.Id);
        await tasklyDbContext.Set<Dictionary<string, object>>("UserTable").AddAsync(tableUserRelations);
        await tasklyDbContext.SaveChangesAsync();
    }

    public async Task RemoveMemberFromTableAsync(Guid tableId, Guid userId)
    {
        var (table, user) = await GetTableAndUserAsync(tableId, userId);
        ValidateTableMembers(table);
        if(table.Members == null)
            throw new InvalidOperationException("Table members list is not initialized.");
        if (!table.Members.Any(m => m.Id == userId))
            throw new ArgumentException("User does not exist in the table");
        var tableUser = await tasklyDbContext.Set<Dictionary<string, object>>("UserTable")
            .FirstOrDefaultAsync(tu => (Guid)tu["TableId"] == table.Id && (Guid)tu["UserId"] == user.Id);
        if (tableUser != null)
            tasklyDbContext.Set<Dictionary<string, object>>("UserTable").Remove(tableUser);
        await tasklyDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TableMemberDto>> GetMembersOfTableAsync(Guid tableId)
    {
        var table = await GetTableIncludeByIdAsync(tableId);
        ValidateTableMembers(table);
        return table.Members.Select(m => new TableMemberDto
        {
            Email = m.Email,
<<<<<<< HEAD
            AvatarName = m.Avatar.ImagePath
        }) ?? Enumerable.Empty<BoardTableMemberDto>();
=======
            AvatarId = m.AvatarId,
            UserName = m.UserName
        }) ?? Enumerable.Empty<TableMemberDto>();
>>>>>>> 500e19cd40af223cf669f4444416bc61471fd104
    }

    private void ValidateTableMembers(TableEntity tableEntity)
    {
        if(tableEntity.Members == null)
            throw new ArgumentException("Table members is not initialized");
    }

    private async Task<(TableEntity table, UserEntity user)> GetTableAndUserAsync(Guid tableId, Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("TableId and UserId must not be empty");
        var table = await GetTableIncludeByIdAsync(tableId);
        var user = await tasklyDbContext.Users.FindAsync(userId);
        if (user == null)
            throw new KeyNotFoundException("User not found");
        return (table, user);
    }
    
    private async Task<TableEntity> GetTableByConditionAsync(Expression<Func<TableEntity, bool>> condition)
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
