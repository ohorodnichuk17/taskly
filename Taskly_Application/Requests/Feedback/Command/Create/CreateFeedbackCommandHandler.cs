using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Feedback.Command.Create;

public class CreateFeedbackCommandHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateFeedbackCommand, ErrorOr<FeedbackEntity>>
{
    public async Task<ErrorOr<FeedbackEntity>> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var feedback = new FeedbackEntity
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Review = request.Review,
                Rating = request.Rating,
                TimeRange = new TimeRangeEntity() { StartTime = DateTime.UtcNow },
            };
            
            await unitOfWork.Feedbacks.CreateAsync(feedback);
            return feedback;
        }
        catch (Exception ex)
        {
            return Error.Failure("CreateFeedbackError ", ex.Message);
        }
    }
}