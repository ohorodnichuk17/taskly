namespace Taskly_Domain.Entities;

public class BoardEntity
{
    public Guid Id { get; init; }
    public required string Name { get; set; } 
    public string? Tag { get; init; }
    public bool IsTeamBoard { get; set; } = false;

    public Guid? BoardTemplateId { get; set; }
    public BoardTemplateEntity? BoardTemplate { get; set; }
    
    public ICollection<UserEntity>? Members { get; set; } = new List<UserEntity>();
    public ICollection<CardListEntity> CardLists { get; set; } = new List<CardListEntity>();
}