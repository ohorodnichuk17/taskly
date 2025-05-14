using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.UserLevel.Query.GetByUserId;

public record GetUserLevelByUserIdQuery(
    Guid UserId) : IRequest<ErrorOr<int>>;