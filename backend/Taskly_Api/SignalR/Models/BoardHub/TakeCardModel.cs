namespace Taskly_Api.SignalR.Models.BoardHub;

public record TakeCardModel(Guid BoardId, Guid CardListId, Guid CardId, Guid UserId);