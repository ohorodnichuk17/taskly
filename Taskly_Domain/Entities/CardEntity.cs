namespace Taskly_Domain.Entities;

public class CardEntity
{
    public Guid Id { get; init; }
    public string? Description { get; set; }
    public string? AttachmentUrl { get; set; } 
    public required string Status { get; set; } 
    public bool IsCompleated { get; set; }
    public Guid? TimeRangeEntityId { get; set; }
    public TimeRangeEntity? TimeRangeEntity { get; set; }
    public ICollection<CommentEntity>? Comments { get; set; } = new List<CommentEntity>();
    public Guid? CardListId { get; set; } 
    public CardListEntity? CardList { get; set; } 
    public Guid? UserId { get; set; }
    public UserEntity? User { get; set; }
}