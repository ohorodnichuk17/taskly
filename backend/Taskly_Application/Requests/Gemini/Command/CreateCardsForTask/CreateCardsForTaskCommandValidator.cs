using FluentValidation;

namespace Taskly_Application.Requests.Gemini.Command.CreateCardsForTask;

public class CreateCardsForTaskCommandValidator : AbstractValidator<CreateCardsForTaskCommand>
{
    public CreateCardsForTaskCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User id is required");

        RuleFor(x => x.BoardId)
            .NotEmpty().WithMessage("Board id is required");

        RuleFor(x => x.Task)
            .NotEmpty().WithMessage("Task cannot be empty")
            .MaximumLength(500).WithMessage("Task must not exceed 500 characters");
    }
}