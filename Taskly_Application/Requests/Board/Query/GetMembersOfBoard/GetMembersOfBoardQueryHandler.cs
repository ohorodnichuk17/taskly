using ErrorOr;
using MediatR;
using Taskly_Application.DTO;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Board.Query.GetMembersOfBoard;

public class GetMembersOfBoardQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMembersOfBoardQuery, ErrorOr<IEnumerable<BoardTableMemberDto>>>
{
    public async Task<ErrorOr<IEnumerable<BoardTableMemberDto>>> Handle(GetMembersOfBoardQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var members = await unitOfWork.Board.GetMembersOfBoardAsync(request.BoardId);
            return members.ToErrorOr();
        }
        catch (Exception ex)
        {
            return Error.Conflict("An error occurred while getting members of the board", ex.Message);
        }
    }
}