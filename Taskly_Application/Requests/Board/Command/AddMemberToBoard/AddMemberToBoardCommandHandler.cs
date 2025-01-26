using MediatR;
using ErrorOr;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;

namespace Taskly_Application.Requests.Board.Command.AddMemberToBoard;

public class AddMemberToBoardCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    : IRequestHandler<AddMemberToBoardCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(AddMemberToBoardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var board = await unitOfWork.Board.GetBoardByIdAsync(request.BoardId);
            var user = await currentUserService.GetUserByEmailAsync(request.MemberEmail);
            // if(board.IsTeamBoard && !board.Members.Any(m => m.Email == request.MemberEmail))
            //     return Error.Unauthorized("You are not a member of this board");
            //if (board.IsTeamBoard)
            //{
                await unitOfWork.Board.AddMemberToBoardAsync(request.BoardId, user.Id);
                //await unitOfWork.SaveChangesAsync("Error adding member to the board.");
            //}
            return Unit.Value;
        }
        catch (Exception ex)
        {
            return Error.Conflict($"Error adding member to the board: {ex.Message}");
        }
    }
}