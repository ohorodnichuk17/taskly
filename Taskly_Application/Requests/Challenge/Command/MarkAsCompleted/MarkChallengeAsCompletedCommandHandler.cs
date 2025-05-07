using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Challenge.Command.MarkAsCompleted;

public class MarkChallengeAsCompletedCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<MarkChallengeAsCompletedCommand, ErrorOr<bool>>
{
    public async Task<ErrorOr<bool>> Handle(MarkChallengeAsCompletedCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.Challenges.CompleteChallengeAsync(request.ChallengeId);
            var challenge = await unitOfWork.Challenges.GetChallengeByIdAsync(request.ChallengeId);
            if (challenge?.UserId is null)
                return Error.Failure("MarkChallengeAsCompletedError", "Challenge is not booked by any user");
            await unitOfWork.UserLevels
                .IncreaseCompletedTasksAsync(challenge.UserId.Value);
            return true;
        }
        catch (Exception ex)
        {
            return Error.Failure("MarkChallengeAsCompletedError", ex.Message);
        }
    }

}