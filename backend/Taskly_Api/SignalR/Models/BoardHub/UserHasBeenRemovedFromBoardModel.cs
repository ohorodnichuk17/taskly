namespace Taskly_Api.SignalR.Models.BoardHub;

public record UserHasBeenRemovedFromBoardModel(Guid BoardId, Guid RemovedUserId, string RemovedUserEmail, string UserEmailWhoRemoved, Guid[] CardsId);
