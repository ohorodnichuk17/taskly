using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Command.Create;

public record CreateBoardCommand(
    Guid Id,
    string Name,
    string Tag,
    bool IsTeamBoard) : IRequest<ErrorOr<BoardEntity>>;