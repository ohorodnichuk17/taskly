using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IAchievementRepository : IRepository<AchievementEntity>
{
    Task<ICollection<AchievementEntity>> CompleateAchievementAsync(UserEntity user);
    Task<ICollection<AchievementEntity>> GetAllAchievementsAsync();
}
