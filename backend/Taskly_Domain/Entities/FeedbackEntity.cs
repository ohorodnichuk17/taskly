namespace Taskly_Domain.Entities;

public class FeedbackEntity
{
    public Guid Id { get; set; }  
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public string Review { get; set; }  
    public int Rating { get; set; }  // Rating given by the user (e.g., 1 to 5 stars)
    
    private DateTime _createdAt;
    public DateTime CreatedAt
    {
        get { return _createdAt; }
        set { _createdAt = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
    }
}