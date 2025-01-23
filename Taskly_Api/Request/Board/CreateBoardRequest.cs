namespace Taskly_Api.Request.Board;

public record CreateBoardRequest(
    string Name,
    string Tag,
    bool IsTeamBoard,
    Guid BoardTemplateId);