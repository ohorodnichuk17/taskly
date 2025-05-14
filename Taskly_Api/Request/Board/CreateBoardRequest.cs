namespace Taskly_Api.Request.Board;

public record CreateBoardRequest(
    Guid UserId,
    string Name,
    string? Tag,
    bool IsTeamBoard,
    Guid BoardTemplateId);