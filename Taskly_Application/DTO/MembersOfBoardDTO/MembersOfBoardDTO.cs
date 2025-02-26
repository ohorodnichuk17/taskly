namespace Taskly_Application.DTO.MembersOfBoardDTO;

public record MembersOfBoardDTO
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public Guid AvatarId { get; set; }
}