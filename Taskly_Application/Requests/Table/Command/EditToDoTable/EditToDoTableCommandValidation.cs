using FluentValidation;

namespace Taskly_Application.Requests.Table.Command.EditToDoTable;

public class EditToDoTableCommandValidation : AbstractValidator<EditToDoTableCommand>
{
    public EditToDoTableCommandValidation()
    {
        RuleFor(x => x.TableId)
            .NotEmpty().WithMessage("Table id is required");

        RuleFor(x => x.TableName)
            .NotEmpty().WithMessage("Table name is required")
            .MaximumLength(50).WithMessage("Table name must not exceed 50 characters");
    }
}