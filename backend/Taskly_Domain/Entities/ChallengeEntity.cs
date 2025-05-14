namespace Taskly_Domain.Entities;

public class ChallengeEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required TimeRangeEntity TimeRange { get; set; }
    public required double Points { get; set; } // нагорода в токенах
    public bool IsBooked { get; set; } // чи заброньована задача
    public bool IsCompleted { get; set; } 
    public bool IsActive { get; set; } // чи активна задача, якщо ні, виводиться дата коли буде активна
    public required string RuleKey { get; set; }
    public int TargetAmount { get; set; } // скільки потрібно виконати (feedbacks - 5, etc)
    public Guid? UserId { get; set; } 
    public UserEntity? User { get; set; } 
}