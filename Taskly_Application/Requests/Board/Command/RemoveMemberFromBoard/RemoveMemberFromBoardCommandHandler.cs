using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;
using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Board.Command.RemoveMemberFromBoard;

public class RemoveMemberFromBoardCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    : IRequestHandler<RemoveMemberFromBoardCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(RemoveMemberFromBoardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await currentUserService.GetUserByEmailAsync(request.MemberEmail);
            await unitOfWork.Board.RemoveMemberFromBoardAsync(request.BoardId, user.Id);
            return Unit.Value;
        }
        catch (Exception ex)
        {
            return Error.Conflict($"Error removing member from the board: {ex.Message}");
        }
    }
}