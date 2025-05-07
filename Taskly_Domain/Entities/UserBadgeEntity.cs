namespace Taskly_Domain.Entities;

public class UserBadgeEntity
{
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    public Guid BadgeId { get; set; }
    public BadgeEntity? Badge { get; set; }
    private DateTime _dateAwarded;
    public DateTime DateAwarded
    {
        get => _dateAwarded;
        set
        {
            if (value > DateTime.UtcNow)
                throw new ArgumentException("DateAwarded cannot be in the future.");
            _dateAwarded = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
}