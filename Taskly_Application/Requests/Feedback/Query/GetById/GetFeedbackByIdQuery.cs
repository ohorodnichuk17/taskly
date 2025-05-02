using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Feedback.Query.GetById;

public record GetFeedbackByIdQuery(Guid FeedbackId) 
    : IRequest<ErrorOr<FeedbackEntity>>;