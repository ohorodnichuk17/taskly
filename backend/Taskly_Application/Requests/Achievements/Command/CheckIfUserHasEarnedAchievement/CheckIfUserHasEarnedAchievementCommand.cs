using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Achievements.Command.CheckIfUserHasEarnedAchievement;

public record CheckIfUserHasEarnedAchievementCommand(Guid UserId) : IRequest<AchievementEntity[]?>;
