using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;
using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Board.Command.RemoveMemberFromBoard;

public class RemoveMemberFromBoardCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RemoveMemberFromBoardCommand, ErrorOr<Guid[]>>
{
    public async Task<ErrorOr<Guid[]>> Handle(RemoveMemberFromBoardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await unitOfWork.Board.RemoveMemberFromBoardAsync(request.BoardId, request.UserId);
            return result.ToArray();
        }
        catch (Exception ex)
        {
            return Error.Conflict($"Error removing member from the board: {ex.Message}");
        }
    }
}