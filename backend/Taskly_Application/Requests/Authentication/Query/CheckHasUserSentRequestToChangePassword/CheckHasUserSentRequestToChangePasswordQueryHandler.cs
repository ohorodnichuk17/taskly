using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Authentication.Query.CheckHasUserSentRequestToChangePassword;

public class CheckHasUserSentRequestToChangePasswordQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<CheckHasUserSentRequestToChangePasswordQuery, ErrorOr<string?>>
{
    public async Task<ErrorOr<string?>> Handle(CheckHasUserSentRequestToChangePasswordQuery request, CancellationToken cancellationToken)
    {
        //var user = await unitOfWork.Authentication.GetUserByEmail(request.Email);

        //if (user == null)
            //return false;

        return await unitOfWork.Authentication.GetUserEmailByChangePasswordKeyAsync(request.Key);
    }
}
