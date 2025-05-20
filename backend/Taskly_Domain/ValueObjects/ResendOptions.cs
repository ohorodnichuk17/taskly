namespace Taskly_Domain.ValueObjects;

public record ResendOptions
{
    public required string ApiKey { get; set; }
}
