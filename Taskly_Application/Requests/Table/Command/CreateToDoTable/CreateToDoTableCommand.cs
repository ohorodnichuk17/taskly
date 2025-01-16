using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Table.Command.CreateToDoTable;

public record CreateToDoTableCommand(Guid UserId) : IRequest<ErrorOr<Guid>>;
