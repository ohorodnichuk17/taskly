using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Services;

public class BadgeService(TasklyDbContext dbContext) : IBadgeService
{
    public async Task CheckAndAssignBadgeAsync(Guid userId)
    {
        var user = await dbContext.Users
            .Include(u => u.Challenges)
            .Include(u => u.Badges)
            .ThenInclude(ub => ub.Badge)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            throw new Exception("User not found");

        var userLevel = await dbContext.UserLevels
            .FirstOrDefaultAsync(ul => ul.UserId == userId);
        if (userLevel == null)
            throw new Exception("UserLevel not found");

        int completedChallenges = userLevel.CompletedTasks;
        

        var allBadges = await dbContext.Badges.ToListAsync();

        foreach (var badge in allBadges)
        {
            bool alreadyHasBadge = user.Badges.Any(b => b.BadgeId == badge.Id);
            
            if (!alreadyHasBadge && completedChallenges >= badge.RequiredTasksToReceiveBadge)
            {
                var userBadge = new UserBadgeEntity
                {
                    UserId = userId,
                    BadgeId = badge.Id,
                    DateAwarded = DateTime.UtcNow
                };

                dbContext.UserBadges.Add(userBadge);
            }
        }

        await dbContext.SaveChangesAsync();
    }
}