using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Badge.Query.GetById;

public class GetBadgeByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetBadgeByIdQuery, ErrorOr<BadgeEntity>>
{
    public async Task<ErrorOr<BadgeEntity>> Handle(GetBadgeByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await unitOfWork.Badges.GetByIdAsync(request.Id);
        }
        catch (Exception ex)
        {
            return Error.Failure("GetAllBadgesError", ex.Message);
        }
    }
}