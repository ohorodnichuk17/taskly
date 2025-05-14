using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Authentication.Command.ChangePassword;

public class ChangePasswordCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangePasswordCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Authentication.GetUserByEmail(request.Email);
        if (user == null)
            return Error.Conflict("User not found");

        return await unitOfWork.Authentication.ChangePasswordAsync(user, request.Password);
    }
}
