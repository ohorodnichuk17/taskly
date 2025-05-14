namespace Taskly_Api.Request.Authenticate;

public record EditAvatarRequest(
    Guid UserId,
    Guid AvatarId);