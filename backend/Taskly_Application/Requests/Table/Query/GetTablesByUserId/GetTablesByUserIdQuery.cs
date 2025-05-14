using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetTablesByUserId;

public record GetTablesByUserIdQuery(Guid UserId) 
    : IRequest<ErrorOr<ICollection<TableEntity>>>;