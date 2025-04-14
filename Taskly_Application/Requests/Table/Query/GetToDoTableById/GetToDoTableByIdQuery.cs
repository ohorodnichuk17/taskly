using MediatR;
using Taskly_Domain.Entities;
using ErrorOr;

namespace Taskly_Application.Requests.Table.Query.GetToDoTableById;

public record GetToDoTableByIdQuery(Guid TableId) : IRequest<ErrorOr<ToDoTableEntity>>;