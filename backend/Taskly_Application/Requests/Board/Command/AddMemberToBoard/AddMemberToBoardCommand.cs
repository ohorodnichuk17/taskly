using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Command.AddMemberToBoard;

public record AddMemberToBoardCommand(
    Guid BoardId,
    string MemberEmail) : IRequest<ErrorOr<UserEntity>>;