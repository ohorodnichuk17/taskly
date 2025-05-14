using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Board.Command.LeaveBoard;

public class LeaveBoardCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<LeaveBoardCommand, ErrorOr<Guid[]>>
{
    public async Task<ErrorOr<Guid[]>> Handle(LeaveBoardCommand request, CancellationToken cancellationToken)
    {

        var result = await unitOfWork.Board.LeaveBoardAsync(request.BoardId, request.UserId);

        if (result == null)
            return Error.Conflict("Something went wrong");

        return result.ToArray();
    }
}
