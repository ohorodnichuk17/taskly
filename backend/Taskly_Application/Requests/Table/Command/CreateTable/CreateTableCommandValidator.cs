using FluentValidation;

namespace Taskly_Application.Requests.Table.Command.CreateTable;

public class CreateTableCommandValidator : AbstractValidator<CreateTableCommand>
{
    public CreateTableCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Table name cannot be empty.")
            .MaximumLength(100)
            .WithMessage("Table name cannot exceed 100 characters.");
    }
}