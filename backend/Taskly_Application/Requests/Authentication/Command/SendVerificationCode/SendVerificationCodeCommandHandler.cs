using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.Json;
using Taskly_Application.Common.Helpers;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IRepository;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain;
using Taskly_Domain.ValueObjects;

namespace Taskly_Application.Requests.Authentication.Command.SendVerificationCode;

public class SendVerificationCodeCommandHandler(
    IUnitOfWork unitOfWork,
    IHttpSenderService httpSenderService) : IRequestHandler<SendVerificationCodeCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
    {

        var isUserExist = await unitOfWork.Authentication.IsUserExist(request.Email);

        if (isUserExist) return Error.Conflict("User with this email already exist");

        await unitOfWork.Authentication.RemovePreviousCodeIfExist(request.Email);

        var code = CodeGenerator.GenerateCode();

        var verificationEmail = await unitOfWork.Authentication.AddVerificationEmail(request.Email, code);
        var props = new Dictionary<string, string>();
        props.Add("[VERIFICATION_CODE]", code);

        var result = await httpSenderService.SendRequestAsync(Constants.VerificateEmail,verificationEmail, props);

        if (result.IsError)
        {
            return Error.Conflict(result.FirstError.Code);
        }
        return verificationEmail;
      
    }
}