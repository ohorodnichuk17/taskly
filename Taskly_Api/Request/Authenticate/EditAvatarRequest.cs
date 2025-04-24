namespace Taskly_Api.Request.Authenticate;

public record EditAvatarRequest(
    Guid Id,
    Guid AvatarId);