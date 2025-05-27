namespace Taskly_Domain.Entities;

public class BadgeEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Icon { get; set; }
    public int RequiredTasksToReceiveBadge { get; set; } 
    public int Level { get; set; } 
}