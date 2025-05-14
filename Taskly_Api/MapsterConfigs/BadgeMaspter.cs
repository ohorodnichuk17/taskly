using Mapster;
using Taskly_Api.Response.Badge;
using Taskly_Domain.Entities;

namespace Taskly_Api.MapsterConfigs;

public class BadgeMaspter : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BadgeEntity, BadgeResponse>()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Name, desp => desp.Name)
            .Map(src => src.Icon, desp => desp.Icon)
            .Map(src => src.RequiredTasksToReceiveBadge, desp => desp.RequiredTasksToReceiveBadge)
            .Map(src => src.Level, desp => desp.Level);
    }
}