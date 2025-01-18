namespace Taskly_Domain.Entities;

public class BoardTemplateEntity // тут будуть фони для дошки
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string ImagePath { get; init; }
    // public Guid? BoardEntityId { get; set; }
    // public BoardEntity Board { get; set; }
    public ICollection<BoardEntity> Boards { get; set; } = new List<BoardEntity>();
}