using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Badge.Query.GetById;

public record GetBadgeByIdQuery(Guid Id)
    : IRequest<ErrorOr<BadgeEntity>>;