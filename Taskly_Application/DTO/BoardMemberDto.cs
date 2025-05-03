namespace Taskly_Application.DTO;

public record BoardMemberDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string AvatarName { get; set; }
    public string? UserName { get; set; }
}