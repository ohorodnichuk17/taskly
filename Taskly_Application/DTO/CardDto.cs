namespace Taskly_Application.DTO;

public class CardDto
{
    public string? Description { get; set; }
    public string? AttachmentUrl { get; set; }
    public required string Status { get; init; }
    public TimeRangeDto? TimeRange { get; set; }
    public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
}