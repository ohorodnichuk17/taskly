namespace Taskly_Api.Response.Card;

public class CommentResponse
{
    public Guid Id { get; init; }
    public string? Text { get; init; }
    public DateTime CreatedAt { get; init; }
}
