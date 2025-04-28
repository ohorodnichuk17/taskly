namespace Taskly_Domain.ValueObjects;

public record EmailSettings
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
