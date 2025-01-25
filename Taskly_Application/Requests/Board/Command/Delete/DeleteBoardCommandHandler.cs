using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Board.Command.Delete;

public class DeleteBoardCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBoardCommand, ErrorOr<bool>>
{
    public async Task<ErrorOr<bool>> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.Board.DeleteAsync(request.BoardId);
            await unitOfWork.SaveChangesAsync("Error while deleting board.");
            return true;
        }
        catch (Exception ex)
        {
            return Error.Conflict("Error while deleting board: ", ex.Message);
        }
    }
}