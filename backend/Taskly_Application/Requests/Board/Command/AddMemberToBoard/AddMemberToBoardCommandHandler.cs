using MediatR;
using ErrorOr;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Command.AddMemberToBoard;

public class AddMemberToBoardCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddMemberToBoardCommand, ErrorOr<UserEntity>>
{
    public async Task<ErrorOr<UserEntity>> Handle(AddMemberToBoardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var isUserExist = await unitOfWork.Authentication.IsUserExist(request.MemberEmail);

            if(isUserExist == false)
                return Error.NotFound("User is not found.");

            var user = await unitOfWork.Authentication.GetUserByEmail(request.MemberEmail);
          
            var isUserOnTheBoard = await unitOfWork.Board.IsUserOnTheBoardAsync(request.BoardId, user!.Id);

            if (isUserOnTheBoard == true)
                return Error.Conflict("User is already on the board.");

            await unitOfWork.Board.AddMemberToBoardAsync(request.BoardId, user.Id);

            
            return user;
        }
        catch (Exception ex)
        {
            return Error.Conflict($"Error adding member to the board: {ex.Message}");
        }
    }
}