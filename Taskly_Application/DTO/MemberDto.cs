namespace Taskly_Application.DTO;

public record MemberDto
{
    public required string Name { get; set; }
    public Guid AvatarId { get; set; }
}