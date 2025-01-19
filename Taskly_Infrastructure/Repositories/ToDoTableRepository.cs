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
}
