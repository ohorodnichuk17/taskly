using FluentValidation;

namespace Taskly_Application.Requests.Authentication.Command.EditUserProfile;

public class EditAvatarCommandValidator : AbstractValidator<EditAvatarCommand>
{
    public EditAvatarCommandValidator()
    {
        RuleFor(r => r.AvatarId)
            .NotEmpty().WithMessage("{PropertyName} must be not empty");
    }
}