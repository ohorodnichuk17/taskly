namespace Taskly_Domain.Other;

public record ChangeCardProps {  
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
};
