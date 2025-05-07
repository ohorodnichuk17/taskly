using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IUserLevelRepository : IRepository<UserLevelEntity>
{
    Task<int> GetLevelPropertyByUserIdAsync(Guid userId);
    Task<int> IncreaseCompletedTasksAsync(Guid userId);
    void Update(UserLevelEntity userLevel);
    Task<UserLevelEntity> GetUserLevelByUserIdAsync(Guid userId);
}
