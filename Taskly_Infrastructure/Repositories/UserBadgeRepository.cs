using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class UserBadgeRepository(TasklyDbContext tasklyDbContext) 
    : Repository<UserBadgeEntity>(tasklyDbContext), IUserBadgeRepository
{
    public async Task<IEnumerable<UserBadgeEntity>> GetAllUserBadgesByUserIdAsync(Guid userId) =>
        await tasklyDbContext.UserBadges
            .Where(u => u.UserId == userId)
            .Include(u => u.Badge)
            .ToListAsync();

    public async Task<UserBadgeEntity?> GetUserBadgeByUserIdAndBadgeIdAsync(Guid userId, Guid badgeId) =>
        await tasklyDbContext.UserBadges
            .Include(u => u.Badge)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.BadgeId == badgeId);
}
