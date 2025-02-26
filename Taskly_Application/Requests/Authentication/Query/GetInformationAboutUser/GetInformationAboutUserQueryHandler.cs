using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Authentication.Query.GetInformationAboutUser;

public class GetInformationAboutUserQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetInformationAboutUserQuery,ErrorOr<UserEntity>>
{
    public async Task<ErrorOr<UserEntity>> Handle(GetInformationAboutUserQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Authentication.GetUserByEmail(request.Email);

        if (user == null)
            return Error.Conflict("User with this email not found");

        if (user.Avatar == null)
            Console.WriteLine("Avatar is null");
        else
            Console.WriteLine("Avatar isn't null");

        return user;
    }
}
