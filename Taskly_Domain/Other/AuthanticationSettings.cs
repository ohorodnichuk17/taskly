namespace Taskly_Domain.Other;

public record AuthanticationSettings
{
    public required string JwtKey { get; init; }
}
