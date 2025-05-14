using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Feedback.Command.Create;

public record CreateFeedbackCommand(
    Guid UserId,
    string Review,
    int Rating) : IRequest<ErrorOr<FeedbackEntity>>;