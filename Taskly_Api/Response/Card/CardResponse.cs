namespace Taskly_Api.Response.Card;

public record CardResponse
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public string? AttachmentUrl { get; init; }
    public required string Status { get; init; } 
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public CommentResponse[]? Comments { get; init; }

}
