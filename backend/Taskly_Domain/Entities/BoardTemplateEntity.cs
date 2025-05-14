namespace Taskly_Domain.Entities;

public class BoardTemplateEntity 
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string ImagePath { get; init; }
    public ICollection<BoardEntity> Boards { get; set; } = new List<BoardEntity>();
}