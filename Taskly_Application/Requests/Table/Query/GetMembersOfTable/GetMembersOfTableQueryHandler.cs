using ErrorOr;
using MediatR;
using Taskly_Application.DTO;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Table.Query.GetMembersOfTable;

public class GetMembersOfTableQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMembersOfTableQuery, ErrorOr<IEnumerable<BoardTableMemberDto>>>
{
    public async Task<ErrorOr<IEnumerable<BoardTableMemberDto>>> Handle(GetMembersOfTableQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var members = await unitOfWork.Table.GetMembersOfTableAsync(request.TableId);
            return members.ToErrorOr();
        }
        catch (Exception ex)
        {
            return Error.Conflict("An error occurred while getting members of the table", ex.Message);
        }
    }
}