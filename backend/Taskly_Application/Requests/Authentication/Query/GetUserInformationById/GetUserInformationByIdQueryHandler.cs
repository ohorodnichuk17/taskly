using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Authentication.Query.GetUserInformationById;

public class GetUserInformationByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserInformationByIdQuery, ErrorOr<UserEntity>>
{
    public async Task<ErrorOr<UserEntity>> Handle(GetUserInformationByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Authentication.GetUserById(request.UserId);
        if(user == null)
            return Error.NotFound("User i not found");

        return user;

        
    }
}
