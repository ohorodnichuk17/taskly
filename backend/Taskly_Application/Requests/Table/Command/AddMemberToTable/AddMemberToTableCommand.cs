using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Table.Command.AddMemberToTable;

public record AddMemberToTableCommand(
    Guid TableId,
    string MemberEmail) : IRequest<ErrorOr<Unit>>;