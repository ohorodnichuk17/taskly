namespace Taskly_Api.Request.Card;

public record TransferCardToAnotherCardListRequest(Guid AnotherCardListId, Guid CardId);
