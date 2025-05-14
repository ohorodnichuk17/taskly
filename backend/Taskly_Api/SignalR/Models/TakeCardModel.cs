namespace Taskly_Api.SignalR.Models;

public record TakeCardModel(Guid BoardId, Guid CardListId, Guid CardId, Guid UserId);