namespace Taskly_Application.DTO;

public record BoardTableMemberDto
{
    public string Email { get; set; }
    public Guid AvatarId { get; set; }
    public string? UserName { get; set; }
}