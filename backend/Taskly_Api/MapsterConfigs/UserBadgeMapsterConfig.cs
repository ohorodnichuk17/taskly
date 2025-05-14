using Mapster;
using Taskly_Api.Response.UserBadge;
using Taskly_Domain.Entities;

namespace Taskly_Api.MapsterConfigs;

public class UserBadgeMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserBadgeEntity, UserBadgeResponse>()
            .Map(dest => dest.Badge, src => src.Badge!.Adapt<BadgeForUserBadgeResponse>())
            .Map(dest => dest.DateAwarded, src => src.DateAwarded);

        config.NewConfig<BadgeEntity, BadgeForUserBadgeResponse>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Icon, src => src.Icon)
            .Map(dest => dest.RequiredTasksToReceiveBadge, src => src.RequiredTasksToReceiveBadge);
    }
}