using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface ITableRepository : IRepository<TableEntity>
{
    Task<TableEntity?> GetTableIncludeById(Guid TableId);
    Task<TableEntity> CreateNewTableAsync(string name);
    Task<UserEntity> AddNewUserToTableAsync(TableEntity table, UserEntity user);
    Task<ICollection<TableEntity>> GetTablesByUserIdAsync(Guid userId);
    Task<bool> DeleteTableAsync(Guid tableId);
    Task<TableEntity> EditTableAsync(Guid tableId, string name);
}
