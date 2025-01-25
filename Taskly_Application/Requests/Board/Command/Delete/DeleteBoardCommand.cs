using MediatR;
using ErrorOr;

namespace Taskly_Application.Requests.Board.Command.Delete;

public record DeleteBoardCommand(Guid BoardId) : IRequest<ErrorOr<bool>>;