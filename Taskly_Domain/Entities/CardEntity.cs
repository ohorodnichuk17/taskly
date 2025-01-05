namespace Taskly_Domain.Entities;

public class CardEntity
{
    public Guid Id { get; init; }
    public string? Description { get; set; }
    public string? AttachmentUrl { get; set; } // можна буде додати картинку або якийь пдф файл
    public required string Status { get; init; } // ToDo, InProgress, Done
    public Guid? TimeRangeEntityId { get; set; }
    public TimeRangeEntity? TimeRangeEntity { get; set; }
    public ICollection<CommentEntity>? Comments { get; set; } = new List<CommentEntity>();
    public Guid CardListId { get; set; } 
    public CardListEntity CardList { get; set; } 
}