namespace Taskly_Application.DTO.TemplateBoardDTOs;

public record TimeRangeDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}