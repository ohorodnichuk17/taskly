using FluentValidation;

namespace Taskly_Application.Requests.Authentication.Command.SendVerificationCode;

public class SendVerificationCodeCommandValidator : AbstractValidator<SendVerificationCodeCommand>
{
    public SendVerificationCodeCommandValidator()
    {
        RuleFor(i => i.Email)
            .NotEmpty().WithMessage("{PropertyName} must be not empty")
            .EmailAddress().WithMessage("Invalid format of email address");
    }
}
