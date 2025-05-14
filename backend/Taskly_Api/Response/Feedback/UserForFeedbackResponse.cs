namespace Taskly_Api.Response.Feedback;

public record UserForFeedbackResponse
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public required Guid AvatarId { get; set; }
}