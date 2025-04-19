using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Table.Command.CreateTable;

public record CreateTableCommand(Guid UserId, string Name) 
    : IRequest<ErrorOr<Guid>>;
