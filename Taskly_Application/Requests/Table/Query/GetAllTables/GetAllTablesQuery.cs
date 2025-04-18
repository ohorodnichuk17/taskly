using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Table.Query.GetAllTables;

public record GetAllTablesQuery() 
    : IRequest<ErrorOr<IEnumerable<TableEntity>>>;