namespace Taskly_Domain.Entities;

public class TableEntity
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public ICollection<TableItemEntity> ToDoItems { get; set; } = new List<TableItemEntity>();
    public ICollection<UserEntity>? Members { get; set; } = new List<UserEntity>();
}