namespace Taskly_Api.SignalR.Models.BoardHub;

public record AddNewCardModel(Guid BoardId, CardModel CardModel);
