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
            var user = await currentUserService.GetUserByEmailAsync(request.MemberEmail);
            await unitOfWork.Board.AddMemberToBoardAsync(request.BoardId, user.Id);
            return Unit.Value;
        }
        catch (Exception ex)
        {
            return Error.Conflict($"Error adding member to the board: {ex.Message}");
        }
    }
}