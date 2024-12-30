namespace Taskly_Domain.Entities;

public class CardListEntity
{
    public Guid Id { get; init; }
    public required string Title { get; set; } // назва головної картки в якій будуть міститися інші картки(CardEntity)
    public ICollection<CardEntity> Cards { get; set; } = new List<CardEntity>();
}