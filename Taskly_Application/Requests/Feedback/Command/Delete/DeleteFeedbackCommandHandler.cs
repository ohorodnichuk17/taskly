using MediatR;
using Taskly_Application.Interfaces;
using ErrorOr;

namespace Taskly_Application.Requests.Feedback.Command.Delete;

public class DeleteFeedbackCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteFeedbackCommand, ErrorOr<bool>>
{
    public async Task<ErrorOr<bool>> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.Feedbacks.DeleteAsync(request.FeedbackId);
            return true;
        }
        catch (Exception ex)
        {
            return Error.Failure("DeleteFeedbackError ", ex.Message);
        }
    }
}