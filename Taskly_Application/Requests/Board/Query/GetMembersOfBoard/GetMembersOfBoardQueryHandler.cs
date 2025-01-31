using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Query.GetMembersOfBoard;

public class GetMembersOfBoardQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetMembersOfBoardQuery, ErrorOr<IEnumerable<UserEntity>>>
{
    public async Task<ErrorOr<IEnumerable<UserEntity>>> Handle(GetMembersOfBoardQuery request, CancellationToken cancellationToken)
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