using Taskly_Domain.Entities;

namespace Taskly_Application.Interfaces.IRepository;

public interface IUserBadgeRepository : IRepository<UserBadgeEntity>
{
    Task<IEnumerable<UserBadgeEntity>> GetAllUserBadgesByUserIdAsync(Guid userId);
    Task<UserBadgeEntity?> GetUserBadgeByUserIdAndBadgeIdAsync(Guid userId, Guid badgeId);
}
