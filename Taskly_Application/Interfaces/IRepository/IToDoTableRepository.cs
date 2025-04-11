using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IToDoTableRepository : IRepository<ToDoTableEntity>
{
    Task<ToDoTableEntity?> GetToDoTableIncludeById(Guid TableId);
    Task<ToDoTableEntity> CreateNewToDoTableAsync(string name);
    Task<UserEntity> AddNewUserToToDoTableAsync(ToDoTableEntity table, UserEntity user);
    Task<ICollection<ToDoTableEntity>> GetToDoTablesByUserIdAsync(Guid userId);
}
