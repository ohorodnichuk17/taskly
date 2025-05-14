using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetAllTableItemsByTableId;

public record GetAllTableItemsByTableIdQuery(Guid TableId) : IRequest<ErrorOr<ICollection<TableItemEntity>>>;
