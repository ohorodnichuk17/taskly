namespace Taskly_Domain.Entities;

public class UserLevelEntity
{
    public Guid Id { get; set; }
    public int Level { get; set; }
    public int CompletedTasks { get; set; }
    private DateTime _dataAchieved;
    public DateTime DataAchieved
    {
        get => _dataAchieved;
        set
        {
            if (value > DateTime.UtcNow)
                throw new ArgumentException("DataAchieved cannot be in the future.");
            _dataAchieved = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
}