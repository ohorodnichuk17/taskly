namespace Taskly_Api.Request.Board;

public record RemoveMemberFromBoardRequest(Guid BoardId, Guid UserId);
