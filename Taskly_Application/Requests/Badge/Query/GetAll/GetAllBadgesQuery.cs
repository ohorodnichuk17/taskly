using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Badge.Query.GetAll;

public record GetAllBadgesQuery()
    : IRequest<ErrorOr<IEnumerable<BadgeEntity>>>;