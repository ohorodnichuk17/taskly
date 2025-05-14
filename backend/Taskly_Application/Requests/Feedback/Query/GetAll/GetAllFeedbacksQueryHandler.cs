using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Feedback.Query.GetAll;

public class GetAllFeedbacksQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllFeedbacksQuery, ErrorOr<IEnumerable<FeedbackEntity>>>
{
    public async Task<ErrorOr<IEnumerable<FeedbackEntity>>> Handle(GetAllFeedbacksQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedbacks = await unitOfWork.Feedbacks.GetAllFeedbacksAsync();
            return feedbacks;
        }
        catch (Exception ex)
        {
            return Error.Failure("GetAllFeedbacksError", ex.Message);
        }
    }
}