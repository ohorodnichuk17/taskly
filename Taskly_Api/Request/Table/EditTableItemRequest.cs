namespace Taskly_Api.Request.Table;

public record EditTableItemRequest(
    Guid Id,
    string? Text,
    string Status,
    DateTime EndTime,
    string? Label,
    bool IsCompleted);