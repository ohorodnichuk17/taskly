namespace Taskly_Domain.Other;

public record EmailSettings
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
