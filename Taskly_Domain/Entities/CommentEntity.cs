namespace Taskly_Domain.Entities;

public class CommentEntity
{
    public Guid Id { get; init; }
    public string? Text { get; set; }
    private DateTime _createdAt;
    public DateTime CreatedAt // це автоматично має задаватися
    {
        get { return _createdAt; }
        set
        {
            _createdAt = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
}