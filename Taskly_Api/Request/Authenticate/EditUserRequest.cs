namespace Taskly_Api.Request.Authenticate;

public record EditUserRequest(
    Guid Id,
    string UserName,
    Guid AvatarId);