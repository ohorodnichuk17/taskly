using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface ITableItemsRepository : IRepository<TableItemEntity>
{
    Task<TableItemEntity> EditTableItemAsync(Guid id, string? text, string status, 
        DateTime endTime, string? label, bool isCompleted);
    
    Task<bool> MarkAsCompletedAsync(Guid id, bool isCompleted);
}
