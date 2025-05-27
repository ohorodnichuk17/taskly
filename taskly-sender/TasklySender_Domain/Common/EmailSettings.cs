namespace TasklySender_Domain.Common;

public record EmailSettings
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
