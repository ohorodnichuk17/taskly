using ErrorOr;
using MediatR;
using Taskly_Application.Common.Helpers;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain;

namespace Taskly_Application.Requests.Authentication.Command.SendVerificationCode;

public class SendVerificationCodeCommandHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService) : IRequestHandler<SendVerificationCodeCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var isUserExist = await unitOfWork.Authentication.IsUserExist(request.Email);

            if (isUserExist) return Error.Conflict("User with this email already exist");

            await unitOfWork.Authentication.RemovePreviousCodeIfExist(request.Email);

            var code = CodeGenerator.GenerateCode();

            var verificationEmail = await unitOfWork.Authentication.AddVerificationEmail(request.Email, code);
            var props = new Dictionary<string, string>();
            props.Add("[VERIFICATION_CODE]", code);

            await emailService.SendHTMLPage(verificationEmail, Constants.VerificateEmail, props);

            return verificationEmail;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error - ",ex.Message);
            return Error.Unexpected(description: "Unexpected error occurred while sending verification code.");
        }
    }
}