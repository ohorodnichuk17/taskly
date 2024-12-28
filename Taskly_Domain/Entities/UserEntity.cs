using Microsoft.AspNetCore.Identity;

namespace Taskly_Domain.Entities;

public class UserEntity : IdentityUser<Guid>
{
    public required AvatarEntity Avatar { get; init; }
    public ICollection<BoardEntity> Boards { get; set; } = new List<BoardEntity>();
    public ICollection<ToDoTableEntity> ToDoTables { get; set; } = new List<ToDoTableEntity>();
}
