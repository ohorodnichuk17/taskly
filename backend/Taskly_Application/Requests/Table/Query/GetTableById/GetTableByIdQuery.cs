using MediatR;
using Taskly_Domain.Entities;
using ErrorOr;

namespace Taskly_Application.Requests.Table.Query.GetTableById;

public record GetTableByIdQuery(Guid TableId) : IRequest<ErrorOr<TableEntity>>;