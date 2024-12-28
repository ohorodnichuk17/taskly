namespace Taskly_Domain.Entities;

public class ToDoItemEntity
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public required string Status { get; init; } // ToDo, InProgress, Done
    public TimeRangeEntity? TimeRange { get; set; }
    public string? Label { get; init; } // На фронтенд буде відображатись як колір (зелений червоний і тд)
    public ICollection<UserEntity> Members { get; set; } = new List<UserEntity>();
    
    public Guid ToDoTableId { get; set; }
    public ToDoTableEntity ToDoTable { get; set; }
}