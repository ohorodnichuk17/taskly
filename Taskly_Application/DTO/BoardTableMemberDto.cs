namespace Taskly_Application.DTO;

public record BoardTableMemberDto
{
<<<<<<< HEAD
    public Guid UserId { get; set; }
    public required string Email { get; set; }
    public required string AvatarName { get; set; }
=======
    public string Email { get; set; }
    public Guid AvatarId { get; set; }
    public string? UserName { get; set; }
>>>>>>> 9a5bb71974aff9265d08e183c80ff215dff752e2
}