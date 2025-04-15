using MediatR;
using ErrorOr;

namespace Taskly_Application.Requests.Table.Command.DeleteToDoTable;

public record DeleteToDoTableCommand(Guid TableId) : IRequest<ErrorOr<bool>>;