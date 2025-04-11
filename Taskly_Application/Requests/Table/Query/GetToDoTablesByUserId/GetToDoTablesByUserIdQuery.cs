using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetToDoTablesByUserId;

public record GetToDoTablesByUserIdQuery(Guid UserId) 
    : IRequest<ErrorOr<ICollection<ToDoTableEntity>>>;