using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Board.Command.AddMemberToBoard;

public record AddMemberToBoardCommand(
    Guid BoardId,
    string MemberEmail) : IRequest<ErrorOr<Unit>>;