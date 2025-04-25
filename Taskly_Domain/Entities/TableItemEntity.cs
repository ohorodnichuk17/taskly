namespace Taskly_Domain.Entities;

public class TableItemEntity
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public required string Status { get; set; } 
    public TimeRangeEntity? TimeRange { get; set; }
    public string? Label { get; set; }
    private bool _isCompleted;

    public bool IsCompleted
    {
        get => _isCompleted;
        set
        {
            _isCompleted = value;
            if (value)
            {
                Status = "Done";
            }
            else
            {
                Status = "ToDo";
            }
        }
    } 
    public ICollection<UserEntity>? Members { get; set; } = new List<UserEntity>();
    public Guid ToDoTableId { get; set; }
    public TableEntity Table { get; set; }
}