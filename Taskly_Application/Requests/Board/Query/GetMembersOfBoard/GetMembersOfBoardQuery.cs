using ErrorOr;
using MediatR;
using Taskly_Application.DTO;

namespace Taskly_Application.Requests.Board.Query.GetMembersOfBoard;

public record GetMembersOfBoardQuery(Guid BoardId) : IRequest<ErrorOr<IEnumerable<BoardTableMemberDto>>>;