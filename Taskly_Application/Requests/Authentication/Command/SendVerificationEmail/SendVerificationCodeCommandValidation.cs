
using FluentValidation;

namespace Taskly_Application.Requests.Authentication.Command.SendVerificationEmail;

public class SendVerificationCodeCommandValidation : AbstractValidator<SendVerificationCodeCommand>
{
    public SendVerificationCodeCommandValidation()
    {
        RuleFor(i => i.Email)
            .NotEmpty().WithMessage("{PropertyName} must be not empty")
            .EmailAddress().WithMessage("Invalid format of email address");
    }
}
