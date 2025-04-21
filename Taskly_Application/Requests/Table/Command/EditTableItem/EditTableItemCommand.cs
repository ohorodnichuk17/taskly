using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Command.EditTableItem;

public record EditTableItemCommand(
    Guid Id,
    string? Text,
    string Status,
    DateTime EndTime,
    string? Label,
    bool IsCompleted) : IRequest<ErrorOr<TableItemEntity>>;