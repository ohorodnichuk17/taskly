using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces;
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

    public async Task<ToDoTableEntity> CreateNewToDoTableAsync()
    {
        var newTable = new ToDoTableEntity() { Id = Guid.NewGuid() };
        if (newTable.Members == null)
            newTable.Members = new List<UserEntity>();

        await CreateAsync(newTable);

        return newTable;
    }

    public async Task<UserEntity> AddNewUserToToDoTableAsync(ToDoTableEntity table,UserEntity user)
    {
        table.Members?.Add(user);
        await SaveAsync(table);
        if (user.ToDoTables == null)
            user.ToDoTables = new List<ToDoTableEntity>();
        user.ToDoTables.Add(table);

        return user;
    }
}
