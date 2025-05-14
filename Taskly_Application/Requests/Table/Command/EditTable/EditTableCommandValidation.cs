using FluentValidation;

namespace Taskly_Application.Requests.Table.Command.EditTable;

public class EditTableCommandValidation : AbstractValidator<EditTableCommand>
{
    public EditTableCommandValidation()
    {
        RuleFor(x => x.TableId)
            .NotEmpty().WithMessage("Table id is required");

        RuleFor(x => x.TableName)
            .NotEmpty().WithMessage("Table name is required")
            .MaximumLength(50).WithMessage("Table name must not exceed 50 characters");
    }
}