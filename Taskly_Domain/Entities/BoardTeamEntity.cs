namespace Taskly_Domain.Entities;

public class BoardTeamEntity
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public Guid IdOfBoardLeader { get; set; }
    public ICollection<UserEntity> Members { get; set; } = new List<UserEntity>();
    public Guid BoardId { get; set; }
    public required BoardEntity Board { get; set; }
}