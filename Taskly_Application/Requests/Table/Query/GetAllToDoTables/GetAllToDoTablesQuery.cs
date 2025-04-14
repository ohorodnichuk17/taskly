using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetAllToDoTables;

public record GetAllToDoTablesQuery() 
    : IRequest<ErrorOr<IEnumerable<ToDoTableEntity>>>;