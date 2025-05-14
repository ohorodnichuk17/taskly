using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface ITableItemsRepository : IRepository<TableItemEntity>
{
    Task<TableItemEntity> EditTableItemAsync(Guid id, string? task, string status, 
        DateTime endTime, string? label);
    
    Task<bool> MarkAsCompletedAsync(Guid id, bool isCompleted);
    
    Task<int> CountCompletedTasksAsync(Guid userId);
}
