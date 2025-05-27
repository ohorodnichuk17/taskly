namespace Taskly_Api.SignalR.Models.BoardHub;

public record TransferCardToAnotherCardListModel(Guid UserId, Guid CardId, Guid FromCardListId, Guid ToCardListId, Guid BoardId, bool IsCompleated);
