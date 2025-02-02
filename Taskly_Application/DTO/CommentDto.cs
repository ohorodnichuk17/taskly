namespace Taskly_Application.DTO;

public record CommentDto
{
    public string? Text { get; set; }
    public DateTime CreatedAt { get; set; }
}