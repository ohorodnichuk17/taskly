using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Command.CreateTableItem;

public record CreateTableItemCommand(string Task,
                                     string Status,
                                     string Label,
                                     List<Guid> Members,
                                     DateTime EndTime,
                                     bool IsCompleted,
                                     Guid TableId) : IRequest<ErrorOr<string>>;
