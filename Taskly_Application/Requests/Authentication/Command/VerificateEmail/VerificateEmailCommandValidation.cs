using FluentValidation;

namespace Taskly_Application.Requests.Authentication.Command.VerificateEmail;

public class VerificateEmailCommandValidation : AbstractValidator<VerificateEmailCommand>
{
    public VerificateEmailCommandValidation()
    {
        RuleFor(i => i.Email)
           .NotEmpty().WithMessage("{PropertyName} must be not empty")
           .EmailAddress().WithMessage("Invalid format of email address");

        RuleFor(i => i.Code)
           .NotEmpty().WithMessage("{PropertyName} must be not empty")
           .Length(7).WithMessage("{PropertyName} length must be 7");
    }
}
