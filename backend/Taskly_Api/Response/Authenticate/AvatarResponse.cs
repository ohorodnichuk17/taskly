namespace Taskly_Api.Response.Authenticate;

public record AvatarResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
