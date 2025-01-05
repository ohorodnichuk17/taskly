using ErrorOr;
using MediatR;
using Taskly_Application.Common.Helpers;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Application.Interfaces.IService;

namespace Taskly_Application.Requests.Authentication.Command.SendVerificationEmail;

public class SendVerificationCodeCommandHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService) : IRequestHandler<SendVerificationCodeCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = await unitOfWork.Authentication.IsUserExist(request.Email);

        if (isUserExist) return Error.Conflict("User with this email already exist");

        var code = CodeGenerator.GenerateCode();

        var verificationEmail = await unitOfWork.Authentication.AddVerificationEmail(request.Email, code);
        await emailService.SendEmail(verificationEmail, "Verification Code", code);

        return verificationEmail;
    }
}
