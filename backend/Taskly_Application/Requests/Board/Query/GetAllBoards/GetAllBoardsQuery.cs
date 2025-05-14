using MediatR;
using Taskly_Domain.Entities;
using ErrorOr;

namespace Taskly_Application.Requests.Board.Query.GetAllBoards;

public record GetAllBoardsQuery() : IRequest<ErrorOr<IEnumerable<BoardEntity>>>;