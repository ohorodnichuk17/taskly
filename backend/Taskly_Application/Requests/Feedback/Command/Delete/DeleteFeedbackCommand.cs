using MediatR;
using ErrorOr;

namespace Taskly_Application.Requests.Feedback.Command.Delete;

public record DeleteFeedbackCommand(
    Guid FeedbackId) : IRequest<ErrorOr<bool>>;