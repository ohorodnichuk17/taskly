namespace Taskly_Api.SignalR.Models.BoardHub;

public record RemoveCardFromCardList(Guid BoardId, Guid CardListId, Guid CardId, Guid UserId);
