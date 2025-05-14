using MediatR;
using ErrorOr;

namespace Taskly_Application.Requests.Table.Command.DeleteTable;

public record DeleteTableCommand(Guid TableId) : IRequest<ErrorOr<bool>>;