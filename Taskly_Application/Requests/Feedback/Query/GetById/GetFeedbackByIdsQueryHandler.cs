using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Feedback.Query.GetById;

public class GetFeedbackByIdsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetFeedbackByIdQuery, ErrorOr<FeedbackEntity>>
{
    public async Task<ErrorOr<FeedbackEntity>> Handle(GetFeedbackByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedback = await unitOfWork.Feedbacks.GetFeedbackById(request.FeedbackId);
            return feedback;
        }
        catch (Exception ex)
        {
            return Error.Failure("GetAllFeedbacksError", ex.Message);
        }
    }
}