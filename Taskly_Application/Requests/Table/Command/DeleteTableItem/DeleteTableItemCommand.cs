using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Table.Command.DeleteTableItem;

public record DeleteTableItemCommand(Guid Id) : IRequest<ErrorOr<bool>>;