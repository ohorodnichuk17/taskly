using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Table.Command.CreateToDoTable;

public record CreateToDoTableCommand(Guid UserId, string Name) 
    : IRequest<ErrorOr<Guid>>;
