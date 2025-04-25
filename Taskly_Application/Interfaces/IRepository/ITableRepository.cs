using Taskly_Application.DTO;
using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface ITableRepository : IRepository<TableEntity>
{
    Task<TableEntity?> GetTableIncludeByIdAsync(Guid tableId);
    Task<TableEntity> CreateNewTableAsync(string name);
    Task<UserEntity> AddNewUserToTableAsync(TableEntity table, UserEntity user);
    Task<ICollection<TableEntity>> GetTablesByUserIdAsync(Guid userId);
    Task<bool> DeleteTableAsync(Guid tableId);
    Task<TableEntity> EditTableAsync(Guid tableId, string name);
    Task AddMemberToTableAsync(Guid tableId, Guid userId);
    Task RemoveMemberFromTableAsync(Guid tableId, Guid userId);
    Task<IEnumerable<BoardTableMemberDto>> GetMembersOfTableAsync(Guid tableId);
}
