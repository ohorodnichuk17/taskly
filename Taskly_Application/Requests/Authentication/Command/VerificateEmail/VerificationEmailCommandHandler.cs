using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;

namespace Taskly_Application.Requests.Authentication.Command.VerificateEmail;

public class VerificationEmailCommandHandler(IAuthenticationRepository authenticationRepository) : IRequestHandler<VerificateEmailCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(VerificateEmailCommand request, CancellationToken cancellationToken)
    {
        var result = await authenticationRepository.IsVerificationEmailExistAndCodeValid(request.Email,request.Code);

        if (!result)
            return Error.Conflict("Invalid or outdated code");

        await authenticationRepository.VerificateEmail(request.Email);

        return request.Email;
    }
}
