using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Board.Command.RemoveMemberFromBoard;

public record RemoveMemberFromBoardCommand(
    Guid BoardId,
    Guid UserId) : IRequest<ErrorOr<Guid[]>>;