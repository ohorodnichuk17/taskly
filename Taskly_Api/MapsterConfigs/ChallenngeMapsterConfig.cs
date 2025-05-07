using Mapster;
using Taskly_Api.Request.Challenge;
using Taskly_Api.Response.Challenge;
using Taskly_Application.Requests.Challenge.Command.Create;
using Taskly_Domain.Entities;

namespace Taskly_Api.MapsterConfigs;

public class ChallenngeMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateChallengeRequest, CreateChallengeCommand>()
            .Map(src => src.Name, desp => desp.Name)
            .Map(src => src.Description, desp => desp.Description)
            .Map(src => src.StartTime, desp => desp.StartTime)
            .Map(src => src.EndTime, desp => desp.EndTime)
            .Map(src => src.Points, desp => desp.Points)
            .Map(src => src.IsActive, desp => desp.IsActive)
            .Map(src => src.RuleKey, desp => desp.RuleKey)
            .Map(src => src.TargetAmount, desp => desp.TargetAmount);

        config.NewConfig<ChallengeEntity, ChallengeResponse>()
            .Map(src => src.Name, desp => desp.Name)
            .Map(src => src.Description, desp => desp.Description)
            .Map(src => src.StartTime, desp => desp.TimeRange.StartTime)
            .Map(src => src.EndTime, desp => desp.TimeRange.EndTime)
            .Map(src => src.IsBooked, desp => desp.IsBooked)
            .Map(src => src.IsCompleted, desp => desp.IsCompleted)
            .Map(src => src.IsActive, desp => desp.IsActive)
            .Map(src => src.RuleKey, desp => desp.RuleKey)
            .Map(src => src.TargetAmount, desp => desp.TargetAmount)
            .Map(src => src.Points, desp => desp.Points)
            .Map(src => src.UserId, desp => desp.UserId)
            .Map(src => src.User, desp => desp.User.Adapt<UserForChallengeResponse>());
    }
}