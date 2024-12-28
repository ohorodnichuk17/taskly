namespace Taskly_Domain.Entities;

public class TimeRangeEntity
{
    public Guid Id { get; init; }
    
    private DateTime _startTime;
    public DateTime StartTime
    {
        get { return _startTime; }
        set { _startTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
    }
    
    private DateTime _endTime;
    public DateTime EndTime
    {
        get { return _endTime; }
        set { _endTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
    }
}