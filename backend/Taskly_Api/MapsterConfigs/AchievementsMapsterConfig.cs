using Mapster;
using Taskly_Api.Response.Achievement;
using Taskly_Domain.Entities;

namespace Taskly_Api.MapsterConfigs;

public class AchievementsMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(AchievementEntity achievement, Guid userId), AchievementResponse>()
            .Map(src => src.Id, desp => desp.achievement.Id)
            .Map(src => src.Name, desp => desp.achievement.Name)
            .Map(src => src.Description, desp => desp.achievement.Description)
            .Map(src => src.Reward, desp => desp.achievement.Reward)
            .Map(src => src.PercentageOfCompletion, desp => desp.achievement.PercentageOfCompletion)
            .Map(src => src.Icon, desp => desp.achievement.Icon)
            .Map(src => src.IsCompleated, desp => desp.achievement.Users.Any(u => u.Id == desp.userId));
    }
}
