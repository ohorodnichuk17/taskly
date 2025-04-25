namespace Taskly_Api.Request.Authenticate;

public record UserForTableItemResponse
{
    public Guid Id { get; init; }
    public required string Email { get; init; }
    public required string Avatar { get; init; }
}
