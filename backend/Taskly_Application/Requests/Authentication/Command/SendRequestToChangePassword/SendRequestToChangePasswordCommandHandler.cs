using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Authentication.Command.SendRequestToChangePassword;

public class SendRequestToChangePasswordCommandHandler(IUnitOfWork unitOfWork, IHttpSenderService httpSenderService) : IRequestHandler<SendRequestToChangePasswordCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(SendRequestToChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Authentication.GetUserByEmail(request.Email);

        if (user == null)
            return Error.Conflict("A user with this email not found");


       var changePasswordKey = Guid.NewGuid();
       await unitOfWork.Authentication.AddChangePasswordKey(request.Email, changePasswordKey);

       var props = new Dictionary<string, string>();
       props.Add("[CHANGE_PASSWORD_KEY]", changePasswordKey.ToString());

        
        var result = await httpSenderService.SendRequestAsync(Constants.ChangePassword, request.Email, props);

        if (result.IsError)
        {
            return Error.Conflict(result.FirstError.Code);
        }

        return changePasswordKey;
    }
}
