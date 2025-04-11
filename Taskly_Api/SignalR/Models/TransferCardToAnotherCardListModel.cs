namespace Taskly_Api.SignalR.Models;

public record TransferCardToAnotherCardListModel(Guid UserId, Guid CardId, Guid FromCardListId, Guid ToCardListId, Guid BoardId);
