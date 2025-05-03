namespace Taskly_Api.Response.Feedback;

public record FeedbackResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public int Rating { get; init; }
    public string Review { get; init; }
    public DateTime CreatedAt { get; init; }
    
    public UserForFeedbackResponse User { get; init; }
}