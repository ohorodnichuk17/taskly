namespace Taskly_Domain.Entities;

public class ChallengeEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required TimeRangeEntity TimeRange { get; set; }
    public required double Points { get; set; } 
    public bool IsBooked { get; set; } 
    public bool IsCompleted { get; set; } 
    public bool IsActive { get; set; } 
    public required string RuleKey { get; set; }
    public int TargetAmount { get; set; } 
    public Guid? UserId { get; set; } 
    public UserEntity? User { get; set; } 
}