namespace Taskly_Application.DTO;

public record BoardTableMemberDto
{
    public Guid UserId { get; set; }
    public required string Email { get; set; }
    public required string AvatarName { get; set; }
}