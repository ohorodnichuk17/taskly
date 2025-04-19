using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface ITableItemsRepository : IRepository<TableItemEntity>
{
    Task<TableItemEntity> EditTableItemAsync(TableItemEntity tableItem);
}
