using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IUserLevelRepository : IRepository<UserLevelEntity>
{
    Task<int> GetUserLevelByUserIdAsync(Guid userId);
    public Task<int> IncreaseCompletedTasksAsync(Guid userId);
}
