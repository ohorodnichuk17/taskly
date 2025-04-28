namespace Taskly_Domain.ValueObjects;

public record ChangeCardProps {  
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
};
