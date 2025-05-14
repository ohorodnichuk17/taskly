using FluentValidation;

namespace Taskly_Application.Requests.Authentication.Command.EditUserProfile;

public class EditAvatarCommandValidation : AbstractValidator<EditAvatarCommand>
{
    public EditAvatarCommandValidation()
    {
        RuleFor(r => r.AvatarId)
            .NotEmpty().WithMessage("{PropertyName} must be not empty");
    }
}