using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Query.GetBoardsByUser;

public record GetBoardsByUserQuery(Guid UserId) : IRequest<ErrorOr<ICollection<BoardEntity>>>;
