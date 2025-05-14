using Taskly_Application.Interfaces;
using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.UserBadge.Query.GetAllUserBadgesByUserId;

public class GetAllUserBadgesByUserIdQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllUserBadgesByUserIdQuery, ErrorOr<IEnumerable<UserBadgeEntity>>>
{
    public async Task<ErrorOr<IEnumerable<UserBadgeEntity>>> Handle(GetAllUserBadgesByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userBadges = (await unitOfWork.UserBadges.GetAllUserBadgesByUserIdAsync(request.UserId)).ToList();
            if (!userBadges.Any())
                return Error.Failure("GetAllUserBadgesByUserIdError", "No badges found for the user");
            return userBadges;
        }
        catch (Exception ex)
        {
            return Error.Failure("GetAllUserBadgesByUserIdError", ex.Message);
        }
    }
}