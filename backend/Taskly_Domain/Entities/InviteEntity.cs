namespace Taskly_Domain.Entities;

public class InviteEntity
{
    public Guid Id { get; set; }
    private DateTime _createdAt;
    public DateTime CreatedAt
    {
        get => _createdAt;
        set => _createdAt = DateTime.SpecifyKind(value, DateTimeKind.Utc); 
    }

    public Guid InvitedByUserId { get; set; }
    public UserEntity InvitedByUser { get; set; }
    
    public Guid RegisteredUserId { get; set; }
    public UserEntity RegisteredUser { get; set; }

    public bool IsSuccessful => RegisteredUserId != Guid.Empty;
}