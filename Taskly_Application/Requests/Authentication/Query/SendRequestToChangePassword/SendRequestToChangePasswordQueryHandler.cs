using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain;

namespace Taskly_Application.Requests.Authentication.Query.SendRequestToChangePassword;

public class SendRequestToChangePasswordQueryHandler(IUnitOfWork unitOfWork, IEmailService emailService) : IRequestHandler<SendRequestToChangePasswordQuery, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(SendRequestToChangePasswordQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Authentication.GetUserByEmail(request.Email);

        if (user == null)
            return Error.Conflict("A user with this email not found");

        //emailService.
       var changePasswordKey = Guid.NewGuid();
       await emailService.SendHTMLPage(request.Email,Constants.ChangePassword, changePasswordKey);

        return changePasswordKey;
    }
}
