namespace Taskly_Domain.Entities;

public class CardCommentEntity
{
    public Guid Id { get; init; }
    public string? Text { get; set; }
    private DateTime _createdAt;
    public DateTime CreatedAt
    {
        get { return _createdAt; }
        set
        {
            _createdAt = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    public Guid CardId { get; set; }
    public CardEntity? Card { get; set; }
}
