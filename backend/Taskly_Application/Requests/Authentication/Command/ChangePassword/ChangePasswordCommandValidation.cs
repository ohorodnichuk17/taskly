using FluentValidation;

namespace Taskly_Application.Requests.Authentication.Command.ChangePassword;

public class ChangePasswordCommandValidation : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidation()
    {
        RuleFor(i => i.Email)
            .NotEmpty().WithMessage("{PropertyName} must be not empty")
            .EmailAddress().WithMessage("Invalid format of email address");

        RuleFor(i => i.Password)
            .NotEmpty().WithMessage("{PropertyName} must be not empty")
            .Equal(i => i.ConfirmPassword).WithMessage("{PropertyName} and Confirm Password must be equal");
    }
}
