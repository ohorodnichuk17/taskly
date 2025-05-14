using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Feedback.Query.GetAll;

public record GetAllFeedbacksQuery() 
    : IRequest<ErrorOr<IEnumerable<FeedbackEntity>>>;