namespace Taskly_Api.Request.Authenticate;

public record ChangePasswordRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string ConfirmPassword { get; init; }
}
