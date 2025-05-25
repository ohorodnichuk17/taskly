using FluentValidation;

namespace Taskly_Application.Requests.Table.Command.AddMemberToTable;

public class AddMemberToTableCommandValidator : AbstractValidator<AddMemberToTableCommand>
{
    public AddMemberToTableCommandValidator()
    {
        RuleFor(command => command.TableId)
            .NotEmpty()
            .WithMessage("Table ID cannot be empty.");

        RuleFor(command => command.MemberEmail)
            .NotEmpty()
            .WithMessage("Member email cannot be empty.")
            .EmailAddress()
            .WithMessage("Invalid email format.");
    }
}