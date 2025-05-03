using ErrorOr;
using MediatR;
using Taskly_Application.DTO;

namespace Taskly_Application.Requests.Table.Query.GetMembersOfTable;

public record GetMembersOfTableQuery(Guid TableId) 
    : IRequest<ErrorOr<IEnumerable<TableMemberDto>>>;