namespace Taskly_Api.Response.Card;

public record CommentResponse
{
    public Guid Id { get; init; }
    public string? Text { get; init; }
    public DateTime CreatedAt { get; init; }
}
