using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Achievements.Query.GetAllAchievementsByUser;

public record GetAllAchievementsByUserQuery : IRequest<ErrorOr<ICollection<AchievementEntity>>>;
