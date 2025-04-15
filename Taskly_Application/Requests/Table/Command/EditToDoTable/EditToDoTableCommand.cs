using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Command.EditToDoTable;

public record EditToDoTableCommand(
    Guid TableId,
    string TableName) : IRequest<ErrorOr<ToDoTableEntity>>;