namespace Taskly_Api.SignalR.Models;

public record LeaveCardModel(Guid BoardId, Guid CardListId, Guid CardId, Guid UserId, string UserName, string UserAvatar);
