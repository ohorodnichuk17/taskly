namespace Taskly_Api.Response.Board;

public record MemberOfBoardResponse(Guid UserId, string Email, string AvatarName);
