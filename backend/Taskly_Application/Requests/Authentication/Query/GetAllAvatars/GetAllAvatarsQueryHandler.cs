using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Authentication.Query.GetAllAvatars;

public class GetAllAvatarsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllAvatarsQuery, ErrorOr<ICollection<AvatarEntity>>>
{
    public async Task<ErrorOr<ICollection<AvatarEntity>>> Handle(GetAllAvatarsQuery request, CancellationToken cancellationToken)
    {
        return (await unitOfWork.Avatar.GetAllAsync()).ToList();
    }
}
