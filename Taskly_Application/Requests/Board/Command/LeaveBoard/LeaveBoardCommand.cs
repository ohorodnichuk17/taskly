using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Board.Command.LeaveBoard;

public record LeaveBoardCommand(Guid BoardId, Guid UserId) : IRequest<ErrorOr<Guid[]>>;
