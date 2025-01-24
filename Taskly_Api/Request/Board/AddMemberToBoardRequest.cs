namespace Taskly_Api.Request.Board;

public record AddMemberToBoardRequest(Guid BoardId, string MemberEmail);