using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;

namespace Taskly_Application.Requests.Authentication.Command.VerificateEmail;

public class VerificationEmailCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<VerificateEmailCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(VerificateEmailCommand request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Authentication.IsVerificationEmailExistAndCodeValid(request.Email,request.Code);

        if (!result)
            return Error.Conflict("Invalid or outdated code");

        await unitOfWork.Authentication.VerificateEmail(request.Email);

        return request.Email;
    }
}
