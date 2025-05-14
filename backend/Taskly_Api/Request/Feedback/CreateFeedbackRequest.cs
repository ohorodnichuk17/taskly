namespace Taskly_Api.Request.Feedback;

public record CreateFeedbackRequest(
    Guid UserId,
    string Review,
    int Rating);