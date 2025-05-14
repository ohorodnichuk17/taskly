using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Achievements.Command.CheckIfUserHasEarnedAchievement;

public class CheckIfUserHasEarnedAchievementCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CheckIfUserHasEarnedAchievementCommand, AchievementEntity[]?>
{
    public async Task<AchievementEntity[]?> Handle(CheckIfUserHasEarnedAchievementCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = await unitOfWork.Authentication.IsUserExist(request.UserId);
        if (isUserExist == false)
            return null;

        var user = await unitOfWork.Authentication.GetByIdAsync(request.UserId);

       
        if (user.PublicKey != null)
        {
            var achievements = new List<AchievementEntity>();
            achievements = (await unitOfWork.Achievements.CompleateAchievementAsync(user)).ToList();
            return achievements.ToArray().Length == 0 ? null : achievements.ToArray();
        }

        return null;
       
    }
}
