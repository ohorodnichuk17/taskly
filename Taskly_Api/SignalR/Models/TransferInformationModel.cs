namespace Taskly_Api.SignalR.Models;

public record TransferInformationModel(Guid UserId,Guid CardId, Guid FromCardListId, Guid ToCardListId);
