namespace Taskly_Api.Request.Authenticate;

public class UserForTableItemResponse
{
    public Guid Id { get; init; }
    public required string Email { get; init; }
    public required string Avatar { get; init; }
}
