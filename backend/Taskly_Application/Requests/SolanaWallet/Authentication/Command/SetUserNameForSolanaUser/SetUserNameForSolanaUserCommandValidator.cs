using FluentValidation;

namespace Taskly_Application.Requests.SolanaWallet.Authentication.Command.SetUserNameForSolanaUser;

public class SetUserNameForSolanaUserCommandValidator : AbstractValidator<SetUserNameForSolanaUserCommand>
{
    public SetUserNameForSolanaUserCommandValidator()
    {
        RuleFor(i => i.PublicKey)
            .NotEmpty().WithMessage("{PropertyName} must be not empty");

        RuleFor(i => i.UserName)
            .NotEmpty().WithMessage("{PropertyName} must be not empty")
            .MinimumLength(3).WithMessage("{PropertyName} must be at least 3 characters long")
            .MaximumLength(20).WithMessage("{PropertyName} must be at most 20 characters long");
    }
}