using Microsoft.AspNetCore.Identity;

namespace Taskly_Domain.Entities;

public class UserEntity : IdentityUser<Guid>
{
    public Guid? AvatarId { get; set; } 
    public AvatarEntity? Avatar { get; set; }  
    public ICollection<BoardEntity> Boards { get; set; } = new List<BoardEntity>();
    public ICollection<TableEntity> ToDoTables { get; set; } = new List<TableEntity>();
    public ICollection<TableItemEntity> ToDoTableItems { get; set; } = new List<TableItemEntity>();
    public ICollection<FeedbackEntity> Feedbacks { get; set; } = new List<FeedbackEntity>();
    public string? PublicKey { get; set; }
}
