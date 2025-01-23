using FluentValidation;

namespace Taskly_Application.Requests.Board.Command.Create;

public class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
{
    public CreateBoardCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Name must not be empty")
            .Length(3, 25).WithMessage("Name must be between 3 and 25 characters");
        
        RuleFor(r => r.Tag)
            .MaximumLength(15).WithMessage("Tag must be less than 15 characters")
            .MinimumLength(2).WithMessage("Tag must be at least 2 characters");
        
        RuleFor(r => r.BoardTemplateId)
            .NotEmpty().WithMessage("Board template id must not be empty");
    }
}