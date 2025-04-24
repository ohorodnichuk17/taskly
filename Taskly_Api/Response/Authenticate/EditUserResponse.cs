namespace Taskly_Api.Response.Authenticate;

public record EditUserResponse(
    string UserName,
    Guid AvatarId);