using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Table.Command.MarkTableItemAsCompleted;

public record MarkTableItemAsCompletedCommand(
    Guid Id,
    bool IsCompleted) : IRequest<ErrorOr<bool>>;