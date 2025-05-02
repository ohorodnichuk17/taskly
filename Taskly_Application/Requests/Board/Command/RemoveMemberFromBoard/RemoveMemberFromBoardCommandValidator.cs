using FluentValidation;
using Taskly_Application.Requests.Board.Command.AddMemberToBoard;

namespace Taskly_Application.Requests.Board.Command.RemoveMemberFromBoard;

public class RemoveMemberFromBoardCommandValidator : AbstractValidator<AddMemberToBoardCommand>
{
    public RemoveMemberFromBoardCommandValidator()
    {
        RuleFor(x => x.BoardId)
            .NotEmpty().WithMessage("Board id is required");

        /*RuleFor(x => x.MemberEmail)
            .NotEmpty().WithMessage("Member email is required")
            .EmailAddress().WithMessage("Member email is not valid");*/
    }
}