namespace Taskly_Application.DTO;

public record TimeRangeDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}