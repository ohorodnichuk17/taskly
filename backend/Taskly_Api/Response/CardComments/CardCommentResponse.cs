namespace Taskly_Api.Response.CardComments;

public class CardCommentResponse
{
    public Guid Id { get; set; }
    public required string Text { get; set; }
    public required string UserName { get; set; }
    public required string UserAvatar { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}
