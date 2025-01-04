using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetAllToDoTableItemsByTableId;

public record GetAllToDoTableItemsByTableIdQuery(Guid ToDoTableId) : IRequest<ErrorOr<ICollection<ToDoItemEntity>>>;
