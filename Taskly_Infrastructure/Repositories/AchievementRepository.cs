using Microsoft.EntityFrameworkCore;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Domain;
using Taskly_Domain.Entities;
using Taskly_Infrastructure.Common.Persistence;

namespace Taskly_Infrastructure.Repositories;

public class AchievementRepository(TasklyDbContext context) : Repository<AchievementEntity>(context), IAchievementRepository
{
    private readonly DbSet<AchievementEntity> _achievementEntitie = context.Set<AchievementEntity>();
    private readonly DbSet<Dictionary<string,object>> _achievementsUsersEntitie = context.Set<Dictionary<string, object>>("UserAchievements");
    public async Task<ICollection<AchievementEntity>> CompleateAchievementAsync(UserEntity user)
    {
        var compleatedAchievements = new List<AchievementEntity>();
        AchievementEntity? achievement = null;

        achievement = await CompleateCardAchievement(user);
        if(achievement != null)
        {
            compleatedAchievements.Add(achievement);
        }

        return compleatedAchievements;
    }
    public async Task<ICollection<AchievementEntity>> GetAllAchievementsAsync()
    {
        return await _achievementEntitie
            .Include(u => u.Users)
            .OrderBy(a => a.Reward)
            .ToListAsync();
    }
    public async Task ChangePercentageOfCompletionOfAllAchievements()
    {
        var achievements = await context.Achievements.ToListAsync();

        foreach (var achievement in achievements)
        {
           await ChangePercentageOfCompletionOfAchievement(achievement);
        }
    }
    private async Task ChangePercentageOfCompletionOfAchievement(AchievementEntity achievement)
    {
        var countOfUsersWithPublicKey = await context.Users.CountAsync(u => u.PublicKey != null);

        if (countOfUsersWithPublicKey != 0)
        {
            var countOfUserHwoCompleatedAchievement = await _achievementsUsersEntitie
                .CountAsync(a => a["AchievementId"].ToString() == achievement.Id.ToString());
            if (countOfUserHwoCompleatedAchievement == 0)
            {
                achievement.PercentageOfCompletion = 100;
            }
            else
            {
                achievement.PercentageOfCompletion = Double.Round((countOfUserHwoCompleatedAchievement * 100) / countOfUsersWithPublicKey, 1);
            }
            await SaveAsync(achievement);
        }
    }
    private async Task<AchievementEntity?> CompleateCardAchievement(UserEntity user)
    {
        var usersCards = await context.Cards.Where(c => c.UserId == user.Id && c.IsCompleated == true).ToListAsync();

        AchievementEntity? achievement = null;
        switch (usersCards.Count)
        {
            case 10:
                achievement = await GetAchievementByNameAsync(Constants.Achievement_FirstHeights);
                achievement = await SaveAchievement(achievement, user);
                break;
            case 30:
                achievement = await GetAchievementByNameAsync(Constants.Achievement_TirelessWorker);
                achievement = await SaveAchievement(achievement, user);
                break;
            case 50:
                achievement = await GetAchievementByNameAsync(Constants.Achievement_MasterOfCards);
                achievement = await SaveAchievement(achievement, user);
                break;
            default:
                break;
        }

        return achievement;
        
    }
    private async Task<AchievementEntity?> SaveAchievement(AchievementEntity? achievement, UserEntity user)
    {
        if (achievement != null && !achievement.Users.Any(u => u.Id == user.Id))
        {
            var newAchievement = new Dictionary<string, object>();
            newAchievement.Add("AchievementId", achievement.Id);
            newAchievement.Add("UserId", user.Id);
            await _achievementsUsersEntitie.AddAsync(newAchievement);

            var countOfUsersWithPublicKey = await context.Users.CountAsync(u => u.PublicKey != null);

            /*if (countOfUsersWithPublicKey != 0)
            {
                var countOfUserHwoCompleatedAchievement = await _achievementsUsersEntitie
                    .CountAsync(a => a["AchievementId"].ToString() == achievement.Id.ToString());
                if(countOfUserHwoCompleatedAchievement == 0)
                {
                    achievement.PercentageOfCompletion = 100;
                }
                else
                {
                    achievement.PercentageOfCompletion = Double.Round((countOfUserHwoCompleatedAchievement * 100) / countOfUsersWithPublicKey, 1);
                }
                await SaveAsync(achievement);
            }*/
            await ChangePercentageOfCompletionOfAchievement(achievement);

            return achievement;
        }
        return null;
    }
    private async Task<AchievementEntity?> GetAchievementByNameAsync(string name)
    {
       return await _achievementEntitie
            .Include(u => u.Users)
            .FirstOrDefaultAsync(a => a.Name == name);
    }
}
