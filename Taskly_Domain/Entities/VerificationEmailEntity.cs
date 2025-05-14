namespace Taskly_Domain.Entities;

public class VerificationEmailEntity : TempEntity
{
    public Guid Id { get; set; }
    public required string Email { get; init; }
    public required string Code { get; init; }
}
