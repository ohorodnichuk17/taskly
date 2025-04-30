using MediatR;
using ErrorOr;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;

namespace Taskly_Application.Requests.Board.Command.AddMemberToBoard;

public class AddMemberToBoardCommandHandler(IUnitOfWork unitOfWork, IUserService userService)
    : IRequestHandler<AddMemberToBoardCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(AddMemberToBoardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.GetUserByEmailAsync(request.MemberEmail);
            if (user == null)
                return Error.NotFound("User is not found.");
            // if(board.IsTeamBoard && !board.Members.Any(m => m.Email == request.MemberEmail))
            //     return Error.Unauthorized("You are not a member of this board");
            var isUserOnTheBoard = await unitOfWork.Board.IsUserOnTheBoardAsync(request.BoardId, user.Id);
            if (isUserOnTheBoard == true)
                return Error.Conflict("User is already on the board.");
            await unitOfWork.Board.AddMemberToBoardAsync(request.BoardId, user.Id);
                //await unitOfWork.SaveChangesAsync("Error adding member to the board.");
            
            return user.Email!;
        }
        catch (Exception ex)
        {
            return Error.Conflict($"Error adding member to the board: {ex.Message}");
        }
    }
}