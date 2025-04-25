using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Table.Command.RemoveMemberFromTable;

public record RemoveMemberFromTableCommand(
    Guid TableId,
    string MemberEmail) : IRequest<ErrorOr<Unit>>;