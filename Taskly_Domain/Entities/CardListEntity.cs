namespace Taskly_Domain.Entities;

public class CardListEntity
{
    public Guid Id { get; init; }
    public required string Title { get; set; }
    public ICollection<CardEntity> Cards { get; set; } = new List<CardEntity>();
    public Guid BoardId { get; set; } 
    public BoardEntity Board { get; set; } 
}