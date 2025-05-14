using MediatR;
using Taskly_Application.Interfaces;
using ErrorOr;

namespace Taskly_Application.Requests.Challenge.Command.Delete;

public class DeleteChallengedCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteChallengedCommand, ErrorOr<bool>>
{
    public async Task<ErrorOr<bool>> Handle(DeleteChallengedCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.Challenges.DeleteAsync(request.Id);
            return true;
        }
        catch (Exception ex)
        {
            return Error.Failure("DeleteChallengeError", ex.Message);
        }
    }
}