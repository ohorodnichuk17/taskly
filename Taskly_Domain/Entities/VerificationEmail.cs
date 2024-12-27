namespace Taskly_Domain.Entities;

public class VerificationEmail
{
    public required string Email { get; init; }
    public required string Code { get; init; }
    public DateTime EndTime { get; init; } = DateTime.UtcNow.AddMinutes(5);
}
