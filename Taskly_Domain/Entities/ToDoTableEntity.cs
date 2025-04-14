namespace Taskly_Domain.Entities;

public class ToDoTableEntity
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public ICollection<ToDoItemEntity> ToDoItems { get; set; } = new List<ToDoItemEntity>();
    public ICollection<UserEntity>? Members { get; set; } = new List<UserEntity>();
}