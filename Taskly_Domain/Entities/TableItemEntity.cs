namespace Taskly_Domain.Entities;

public class TableItemEntity
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public required string Status { get; init; } // ToDo, InProgress, Done
    public TimeRangeEntity? TimeRange { get; set; }
    public string? Label { get; init; }
    public bool IsCompleted { get; set; } = false;
    public ICollection<UserEntity> Members { get; set; } = new List<UserEntity>();
    public Guid ToDoTableId { get; set; }
    public TableEntity Table { get; set; }
}