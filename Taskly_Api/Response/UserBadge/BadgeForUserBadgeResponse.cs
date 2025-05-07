namespace Taskly_Api.Response.UserBadge;

public record BadgeForUserBadgeResponse(
    string Name,
    string Icon,
    int RequiredTasksToReceiveBadge);