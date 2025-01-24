using FluentValidation;

namespace Taskly_Application.Requests.Board.Command.AddMemberToBoard;

public class AddMemberToBoardCommandValidator : AbstractValidator<AddMemberToBoardCommand>
{
    public AddMemberToBoardCommandValidator()
    {
        RuleFor(x => x.BoardId)
            .NotEmpty().WithMessage("Board id is required");

        RuleFor(x => x.MemberEmail)
            .NotEmpty().WithMessage("Member email is required")
            .EmailAddress().WithMessage("Member email is not valid");
    }
}