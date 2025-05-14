using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.UserBadge.Query.GetAllUserBadgesByUserId;

public record GetAllUserBadgesByUserIdQuery(
    Guid UserId) : IRequest<ErrorOr<IEnumerable<UserBadgeEntity>>>;