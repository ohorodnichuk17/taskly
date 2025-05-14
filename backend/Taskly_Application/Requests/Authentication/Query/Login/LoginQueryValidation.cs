using FluentValidation;

namespace Taskly_Application.Requests.Authentication.Query.Login;

public class LoginQueryValidation : AbstractValidator<LoginQuery>
{
    public LoginQueryValidation()
    {
        RuleFor(i => i.Email)
            .NotEmpty().WithMessage("{PropertyName} must be not empty")
            .EmailAddress().WithMessage("Invalid format of email address");

        RuleFor(i => i.Password)
            .NotEmpty().WithMessage("{PropertyName} must be not empty");
    }
}
