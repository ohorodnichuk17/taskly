namespace Taskly_Api.Response.UserBadge;

public record UserBadgeResponse(
    BadgeForUserBadgeResponse Badge,
    DateTime DateAwarded);