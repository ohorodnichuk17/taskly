using FluentValidation;

namespace Taskly_Application.Requests.Authentication.Command.EditUserProfile;

public class EditUserProfileCommandValidation : AbstractValidator<EditUserProfileCommand>
{
    public EditUserProfileCommandValidation()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("{PropertyName} must be not empty")
            .EmailAddress().WithMessage("Invalid format of email address");
        RuleFor(r => r.UserName)
            .NotEmpty().WithMessage("{PropertyName} must be not empty")
            .MinimumLength(3).WithMessage("{PropertyName} must be at least 3 characters long")
            .MaximumLength(20).WithMessage("{PropertyName} must be at most 20 characters long");
        RuleFor(r => r.AvatarId)
            .NotEmpty().WithMessage("{PropertyName} must be not empty");
    }
}