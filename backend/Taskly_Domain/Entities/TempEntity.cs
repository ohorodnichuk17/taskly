namespace Taskly_Domain.Entities;

public class TempEntity
{
    public DateTime EndTime { get; init; } = DateTime.UtcNow.AddMinutes(5);
}
