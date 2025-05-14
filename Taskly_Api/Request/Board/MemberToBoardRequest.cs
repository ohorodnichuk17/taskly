namespace Taskly_Api.Request.Board;

public record MemberToBoardRequest(Guid BoardId, string MemberEmail);