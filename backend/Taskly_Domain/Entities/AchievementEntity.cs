namespace Taskly_Domain.Entities;

public class AchievementEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public float Reward { get; set; }
    public double PercentageOfCompletion { get; set; }
    public required string Icon { get; set; }
    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}
