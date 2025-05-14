using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;

namespace Taskly_Application.Requests.Challenge.Command.MarkAsCompleted;

public class MarkChallengeAsCompletedCommandHandler(IUnitOfWork unitOfWork, IBadgeService badgeService)
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

            var userId = challenge.UserId.Value;
            var userLevel = await unitOfWork.UserLevels.GetUserLevelByUserIdAsync(userId);
            
            userLevel.CompletedTasks++;

            if (userLevel.CompletedTasks >= 50)
                userLevel.Level = 3;
            else if (userLevel.CompletedTasks >= 15)
                userLevel.Level = 2;
            else if (userLevel.CompletedTasks >= 5)
                userLevel.Level = 1;

            unitOfWork.UserLevels.Update(userLevel); 
            await unitOfWork.SaveChangesAsync("MarkChallengeAsCompletedError"); 

            await badgeService.CheckAndAssignBadgeAsync(userId);
            
            return true;
        }
        catch (Exception ex)
        {
            return Error.Failure("MarkChallengeAsCompletedError", ex.Message);
        }
    }

}