namespace Taskly_Domain.Entities;

public class BoardEntity
{
    public Guid Id { get; init; }
    public required string Name { get; set; } // назва дошки
    public string Tag { get; init; } // тег дошки який ми самі задаємо, типу при створенні буде просто писати Template

    public BoardTeamEntity BoardTeam { get; set; }
    public ICollection<BoardTemplateEntity> BoardTemplates { get; set; } = new List<BoardTemplateEntity>();
    public ICollection<CardListEntity> CardLists { get; set; } = new List<CardListEntity>();
    
}