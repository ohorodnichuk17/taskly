using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Command.CreateToDoTableItem;

public record CreateToDoTableItemCommand(string Task,
                                     string Status,
                                     string Label,
                                     List<Guid> Members,
                                     DateTime EndTime,
                                     Guid ToDoTableId) : IRequest<ErrorOr<string>>;
