namespace Taskly_Api.SignalR.Models.CardCommentsHub;

public record LeaveCommentModel(Guid CardId, Guid UserId, string Text);
