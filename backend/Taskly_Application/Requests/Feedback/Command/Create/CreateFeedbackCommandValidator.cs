using FluentValidation;

namespace Taskly_Application.Requests.Feedback.Command.Create;

public class CreateFeedbackCommandValidator : AbstractValidator<CreateFeedbackCommand>
{
    public CreateFeedbackCommandValidator()
    {
        RuleFor(r => r.Review)
            .NotEmpty().WithMessage("Review must not be empty")
            .Length(3, 500).WithMessage("Review must be between 3 and 500 characters");

        RuleFor(r => r.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5");
        
        RuleFor(r => r.UserId)
            .NotEmpty().WithMessage("User id must not be empty");
    }
}