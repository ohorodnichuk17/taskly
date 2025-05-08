namespace Taskly_Api.Response.Authenticate;

public record InformationAboutUserResponse
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string AvatarName { get; set; }
    public required string Token { get; set; }
    public required string Role { get; set; }
}
