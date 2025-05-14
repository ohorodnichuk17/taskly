using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Board.Command.DeleteBoard;

public record DeleteBoardCommand(Guid BoardId) : IRequest<ErrorOr<bool>>;