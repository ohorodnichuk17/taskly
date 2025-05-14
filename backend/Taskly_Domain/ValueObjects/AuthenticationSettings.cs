namespace Taskly_Domain.ValueObjects;

public record AuthenticationSettings
{
    public required string JwtKey { get; init; }
}
