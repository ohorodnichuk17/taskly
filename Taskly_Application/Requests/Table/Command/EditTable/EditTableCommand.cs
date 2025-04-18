using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Command.EditTable;

public record EditTableCommand(
    Guid TableId,
    string TableName) : IRequest<ErrorOr<TableEntity>>;