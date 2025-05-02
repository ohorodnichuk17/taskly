namespace Taskly_Api.SignalR.Models;

public record UserHasLeftBoardModel(Guid BoardId, Guid[] CardsId);
