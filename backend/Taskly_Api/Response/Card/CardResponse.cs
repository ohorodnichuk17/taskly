namespace Taskly_Api.Response.Card;

public record CardResponse
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public string? AttachmentUrl { get; init; }
    public required string Status { get; init; }
    public bool IsCompleated { get; set; }
    public Guid? UserId { get; init; }
    public string? UserAvatar { get; init; }
    public string? UserName { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public CommentResponse[]? Comments { get; init; }

}
