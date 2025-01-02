namespace Taskly_Domain.Entities;

public class AvatarEntity
{
    public Guid Id { get; init; }
    public required string ImagePath { get; init; }
    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}