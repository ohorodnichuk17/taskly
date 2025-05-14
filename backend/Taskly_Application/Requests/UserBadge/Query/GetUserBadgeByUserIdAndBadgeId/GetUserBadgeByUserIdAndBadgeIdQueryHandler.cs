using Taskly_Application.Interfaces;
using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.UserBadge.Query.GetUserBadgeByUserIdAndBadgeId;

public class GetUserBadgeByUserIdAndBadgeIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetUserBadgeByUserIdAndBadgeIdQuery, ErrorOr<UserBadgeEntity>>
{
    public async Task<ErrorOr<UserBadgeEntity>> Handle(GetUserBadgeByUserIdAndBadgeIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userBadge = await unitOfWork.UserBadges.GetUserBadgeByUserIdAndBadgeIdAsync(request.UserId, request.BadgeId);
            if (userBadge is null)
                return Error.Failure("GetUserBadgeByUserIdAndBadgeIdError", "No badge found for the user");
            return userBadge;
        }
        catch (Exception ex)
        {
            return Error.Failure("GetUserBadgeByUserIdAndBadgeIdError", ex.Message);
        }
    }
}