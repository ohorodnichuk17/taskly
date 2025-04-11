namespace Taskly_Application.DTO.UserDTO;

public record EditUserDTO
{
    public string Email { get; set; } 
    public string UserName { get; set; }
    public Guid AvatarId { get; set; }
    
    public EditUserDTO() { }
    
    public EditUserDTO(string updatedUserEmail, string updatedUserUserName, Guid updatedUserAvatarId)
    {
        Email = updatedUserEmail;
        UserName = updatedUserUserName;
        AvatarId = updatedUserAvatarId;
    }
}