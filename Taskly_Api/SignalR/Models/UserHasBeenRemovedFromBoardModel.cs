namespace Taskly_Api.SignalR.Models;

public record UserHasBeenRemovedFromBoardModel(Guid BoardId,Guid RemovedUserId, string RemovedUserEmail, string UserEmailWhoRemoved, Guid[] CardsId);
