namespace Taskly_Api.Response.Badge;

public record BadgeResponse(
    Guid Id,
    string Name,
    string Icon,
    int RequiredTasksToReceiveBadge,
    int Level);