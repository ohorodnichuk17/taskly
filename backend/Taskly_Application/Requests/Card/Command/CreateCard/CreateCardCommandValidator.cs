using FluentValidation;

namespace Taskly_Application.Requests.Card.Command.CreateCard;

public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
{
    public CreateCardCommandValidator()
    {
        RuleFor(i => i.CardListId)
            .NotEmpty().WithMessage("{PropertyName} must be not empty");

        RuleFor(i => i.Task)
            .NotEmpty().WithMessage("{PropertyName} must be not empty")
            .MaximumLength(100).WithMessage("{PropertyName} must be less than 100 characters");

        RuleFor(i => i.Deadline)
            .GreaterThan(DateTime.UtcNow).WithMessage("{PropertyName} must be in the future");
    }
}