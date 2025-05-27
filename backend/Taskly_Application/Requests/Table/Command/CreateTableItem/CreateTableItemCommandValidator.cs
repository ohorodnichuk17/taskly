using FluentValidation;

namespace Taskly_Application.Requests.Table.Command.CreateTableItem;

public class CreateTableItemCommandValidator : AbstractValidator<CreateTableItemCommand>
{
    public CreateTableItemCommandValidator()
    {
        RuleFor(command => command.TableId)
            .NotEmpty()
            .WithMessage("Table ID cannot be empty.");

        RuleFor(command => command.Task)
            .NotEmpty()
            .WithMessage("Task cannot be empty.")
            .MaximumLength(200)
            .WithMessage("Task cannot exceed 200 characters.");
        
        RuleFor(command => command.Status)
            .NotEmpty()
            .WithMessage("Status cannot be empty.")
            .Must(status => status == "Done" || status == "InProgress" || status == "ToDo")
            .WithMessage("Status must be either 'Done', 'InProgress', or 'ToDo'.");
        
        RuleFor(command => command.Label)
            .NotEmpty()
            .WithMessage("Label cannot be empty.")
            .MaximumLength(50)
            .WithMessage("Label cannot exceed 50 characters.");

        RuleFor(command => command.EndTime)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("End time must be in the future.");

        RuleFor(command => command.IsCompleted)
            .NotNull()
            .WithMessage("IsCompleted cannot be null.");
    }
}