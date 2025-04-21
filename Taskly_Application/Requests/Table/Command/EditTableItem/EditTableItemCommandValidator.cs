using FluentValidation;

namespace Taskly_Application.Requests.Table.Command.EditTableItem;

public class EditTableItemCommandValidator : AbstractValidator<EditTableItemCommand>
{
    public EditTableItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Table item id is required");

        RuleFor(x => x.Text)
            .MaximumLength(50).WithMessage("Text must not exceed 50 characters");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .Must(status => status == "ToDo" || status == "InProgress" || status == "Done")
            .WithMessage("Status must be either 'ToDo', 'InProgress', or 'Done'");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("End time is required")
            .GreaterThan(DateTime.UtcNow).WithMessage("End time must be in the future");
    }
}