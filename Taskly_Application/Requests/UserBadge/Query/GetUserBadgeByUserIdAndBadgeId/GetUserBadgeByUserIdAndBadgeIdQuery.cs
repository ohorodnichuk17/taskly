using MediatR;
using Taskly_Domain.Entities;
using ErrorOr;

namespace Taskly_Application.Requests.UserBadge.Query.GetUserBadgeByUserIdAndBadgeId;

public record GetUserBadgeByUserIdAndBadgeIdQuery(
    Guid UserId,
    Guid BadgeId) : IRequest<ErrorOr<UserBadgeEntity>>;