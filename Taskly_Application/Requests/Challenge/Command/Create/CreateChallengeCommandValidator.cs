using FluentValidation;

namespace Taskly_Application.Requests.Challenge.Command.Create;

public class CreateChallengeCommandValidator : AbstractValidator<CreateChallengeCommand>
{
    public CreateChallengeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(20)
            .WithMessage("Name must not exceed 20 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.StartTime)
            .NotEmpty()
            .WithMessage("Start time is required.");

        RuleFor(x => x.EndTime)
            .NotEmpty()
            .WithMessage("End time is required.")
            .GreaterThan(x => x.StartTime)
            .WithMessage("End time must be greater than start time.");

        RuleFor(x => x.Points)
            .GreaterThan(0)
            .WithMessage("Points must be greater than zero.");
        
        RuleFor(x => x.TargetAmount)
            .GreaterThan(0)
            .WithMessage("Target amount must be greater than zero.");
        
        RuleFor(x => x.RuleKey)
            .Must(rule => AllowedRuleKeys.Contains(rule))
            .WithMessage("Invalid RuleKey. Allowed values are: " + string.Join(", ", AllowedRuleKeys));
    }
    
    private static readonly string[] AllowedRuleKeys =
    {
        "Taskly:CompletedTableItems",
        "Taskly:CountUserFeedbacks"
    };
}