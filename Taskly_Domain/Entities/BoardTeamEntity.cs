namespace Taskly_Domain.Entities;

public class BoardTeamEntity
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public Guid LeaderOfBoardId { get; set; }
    public ICollection<UserEntity> Members { get; set; } = new List<UserEntity>();
    public required BoardEntity Board { get; set; }
}