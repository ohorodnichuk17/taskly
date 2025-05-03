namespace Taskly_Application.DTO;

public record TableMemberDto
{
    public string Email { get; set; }
    public Guid? AvatarId { get; set; }
    public string? UserName { get; set; }
}