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
            Console.WriteLine($"[MarkChallengeAsCompleted] Handling command for ChallengeId: {request.ChallengeId}");

            await unitOfWork.Challenges.CompleteChallengeAsync(request.ChallengeId);

            Console.WriteLine($"[MarkChallengeAsCompleted] Challenge {request.ChallengeId} marked as completed successfully.");

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[MarkChallengeAsCompleted] Error occurred while completing challenge {request.ChallengeId}: {ex.Message}");
            Console.WriteLine($"[MarkChallengeAsCompleted] StackTrace: {ex.StackTrace}");

            return Error.Failure("MarkChallengeAsCompletedError", ex.Message);
        }
    }

}