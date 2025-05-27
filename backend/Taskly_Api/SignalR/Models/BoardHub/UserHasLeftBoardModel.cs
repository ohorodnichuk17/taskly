namespace Taskly_Api.SignalR.Models.BoardHub;

public record UserHasLeftBoardModel(Guid BoardId, Guid[] CardsId);
