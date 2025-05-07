namespace Taskly_Domain.Entities;

public class BadgeEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Icon { get; set; }
    public int RequiredTasksToReceiveBadge { get; set; } // кількість завдань для отримання значка
    public int Level { get; set; } // рівень значка (1.2.3) 
}