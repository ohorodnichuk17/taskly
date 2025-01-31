namespace Taskly_Application.DTO.TemplateBoardDTOs;

public record MemberDto
{
    public required string Name { get; set; }
    public Guid AvatarId { get; set; }
}