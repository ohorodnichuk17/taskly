using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class TableItemsRepository(TasklyDbContext tasklyDbContext) : Repository<TableItemEntity>(tasklyDbContext), ITableItemsRepository
{
    public async Task<TableItemEntity> EditTableItemAsync(Guid id, string? task, 
        string status, DateTime endTime, string? label)
    {
        var tableItemToEdit = await tasklyDbContext.ToDoItems
            .Include(x => x.TimeRange)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (tableItemToEdit == null)
            throw new InvalidOperationException("Table item not found.");

        tableItemToEdit.Text = task;
        
        if (!new[] { "ToDo", "InProgress", "Done" }.Contains(status))
        {
            throw new ArgumentException("Invalid status value.");
        }
        
        if (tableItemToEdit.IsCompleted && tableItemToEdit.Status != "Done")
        {
            tableItemToEdit.Status = "Done";
        }
        else if (!tableItemToEdit.IsCompleted)
        {
            tableItemToEdit.Status = status;
        }

        if (tableItemToEdit.TimeRange.EndTime != null && endTime != null)
        {
            tableItemToEdit.TimeRange.EndTime = endTime;
        }

        tableItemToEdit.Label = label;
        
        await SaveAsync(tableItemToEdit);
        
        return tableItemToEdit;
    }

    public async Task<bool> MarkAsCompletedAsync(Guid id, bool isCompleted)
    {
        var tableItem = await tasklyDbContext.ToDoItems
            .FirstOrDefaultAsync(x => x.Id == id);
        if (tableItem == null)
        {
            throw new InvalidOperationException("Table item not found.");
        }
        tableItem.IsCompleted = isCompleted;
        tableItem.Status = isCompleted ? "Done" : tableItem.Status;
        await SaveAsync(tableItem);
        return true;
    }
}
