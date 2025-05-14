using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Query.GetBoardById;

public record GetBoardByIdQuery(Guid Id) 
    : IRequest<ErrorOr<BoardEntity>>;