namespace Taskly_Api.SignalR.Models;

public record RemoveCardFromCardList(Guid BoardId, Guid CardListId, Guid CardId, Guid UserId);
