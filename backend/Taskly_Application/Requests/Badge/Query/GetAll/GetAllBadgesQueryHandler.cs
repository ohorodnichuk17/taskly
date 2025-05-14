using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Badge.Query.GetAll;

public class GetAllBadgesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllBadgesQuery, ErrorOr<IEnumerable<BadgeEntity>>>
{
    public async Task<ErrorOr<IEnumerable<BadgeEntity>>> Handle(GetAllBadgesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var badges = (await unitOfWork.Badges.GetAllAsync()).ToList();
            if (badges.Count == 0)
                return Error.NotFound("NoBadgesFound", "No badges found.");
            return badges;
        }
        catch (Exception ex)
        {
            return Error.Failure("GetAllBadgesError", ex.Message);
        }
    }
}