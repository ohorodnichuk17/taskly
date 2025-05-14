using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Achievements.Query.GetAllAchievementsByUser;

public class GetAllAchievementsByUserHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllAchievementsByUserQuery, ErrorOr<ICollection<AchievementEntity>>>
{
    public async Task<ErrorOr<ICollection<AchievementEntity>>> Handle(GetAllAchievementsByUserQuery request, CancellationToken cancellationToken)
    {
        var achievements = await unitOfWork.Achievements.GetAllAchievementsAsync();

        return achievements.ToErrorOr();
    }
}
