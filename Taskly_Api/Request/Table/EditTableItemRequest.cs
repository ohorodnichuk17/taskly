namespace Taskly_Api.Request.Table;

public record EditTableItemRequest(
    Guid Id,
    string? Task,
    string Status,
    DateTime EndTime,
    string? Label);