namespace Taskly_Domain.Other;

public record AuthenticationSettings
{
    public required string JwtKey { get; init; }
}
