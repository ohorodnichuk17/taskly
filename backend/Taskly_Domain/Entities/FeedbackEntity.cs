namespace Taskly_Domain.Entities;

public class FeedbackEntity
{
    public Guid Id { get; set; }  
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public string Review { get; set; }  
    public int Rating { get; set; }  
    
    private DateTime _createdAt;
    public DateTime CreatedAt
    {
        get { return _createdAt; }
        set { _createdAt = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
    }
}